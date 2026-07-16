let currentPage = 1;

async function loadOrders(page) {
  currentPage = page;
  const container = document.getElementById('orders-container');
  const pagContainer = document.getElementById('pagination-container');
  renderLoading(container);

  try {
    const data = await ApiClient.get('/api/Order/Orders', { page: page, pageSize: CONFIG.PAGE_SIZE });
    if (!data.items || data.items.length === 0) {
      renderEmpty(container, 'No orders found');
      pagContainer.innerHTML = '';
      return;
    }

    let html = '<div class="table-wrapper"><table><thead><tr><th>Orderer</th><th>Status</th><th>Total</th><th>Delivery</th><th>Items</th></tr></thead><tbody>';
    data.items.forEach(item => {
      const itemsList = (item.items || []).map(i => Utils.escapeHtml(i.name)).join(', ');
      html += '<tr>';
      html += '<td>' + Utils.escapeHtml(item.ordererName) + '</td>';
      html += '<td><span class="badge ' + Utils.getStatusBadgeClass(item.status) + '">' + Utils.escapeHtml(item.status) + '</span></td>';
      html += '<td>' + Utils.formatCurrency(item.totalPrice) + '</td>';
      html += '<td>' + Utils.formatDate(item.deliveryDate) + '</td>';
      html += '<td title="' + itemsList + '">' + (item.items ? item.items.length : 0) + ' product(s)</td>';
      html += '</tr>';
    });
    html += '</tbody></table></div>';
    container.innerHTML = html;
    renderPagination(pagContainer, { pageNumber: data.pageNumber, pageSize: data.pageSize, totalPageCount: data.totalPageCount }, loadOrders);
  } catch (err) {
    renderError(container, err.message);
  }
}

document.addEventListener('DOMContentLoaded', () => {
  Layout.render('All Orders');
  loadOrders(1);
});
