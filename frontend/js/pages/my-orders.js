let currentPage = 1;
let userGuid = null;

async function loadOrders(page) {
  if (!userGuid) return;
  currentPage = page;
  const container = document.getElementById('orders-container');
  const pagContainer = document.getElementById('pagination-container');
  renderLoading(container);

  try {
    const data = await ApiClient.get('/api/Order/Orders' + userGuid, { page: page, pageSize: CONFIG.PAGE_SIZE });
    if (!data.items || data.items.length === 0) {
      renderEmpty(container, 'No orders found for this user');
      pagContainer.innerHTML = '';
      return;
    }

    let html = '<div class="table-wrapper"><table><thead><tr><th>Orderer</th><th>Status</th><th>Total</th><th>Delivery</th><th>Items</th></tr></thead><tbody>';
    data.items.forEach(item => {
      html += '<tr>';
      html += '<td>' + Utils.escapeHtml(item.ordererName) + '</td>';
      html += '<td><span class="badge ' + Utils.getStatusBadgeClass(item.status) + '">' + Utils.escapeHtml(item.status) + '</span></td>';
      html += '<td>' + Utils.formatCurrency(item.totalPrice) + '</td>';
      html += '<td>' + Utils.formatDate(item.deliveryDate) + '</td>';
      html += '<td>' + (item.items ? item.items.length : 0) + ' product(s)</td>';
      html += '</tr>';
    });
    html += '</tbody></table></div>';
    container.innerHTML = html;
    renderPagination(pagContainer, { pageNumber: data.pageNumber, pageSize: data.pageSize, totalPageCount: data.totalPageCount }, loadOrders);
  } catch (err) {
    renderError(container, err.message);
  }
}

function fetchUserOrders() {
  const input = document.getElementById('user-guid-input');
  const val = input.value.trim();
  Utils.clearFieldError(input);

  if (!val) {
    Utils.showFieldError(input, 'Please enter a User ID');
    return;
  }

  userGuid = val;
  document.getElementById('user-id-display').textContent = val;
  document.getElementById('guid-input-section').classList.add('hidden');
  document.getElementById('orders-section').classList.remove('hidden');
  loadOrders(1);
}

function changeUser() {
  userGuid = null;
  document.getElementById('user-id-display').textContent = '';
  document.getElementById('guid-input-section').classList.remove('hidden');
  document.getElementById('orders-section').classList.add('hidden');
  document.getElementById('orders-container').innerHTML = '';
  document.getElementById('pagination-container').innerHTML = '';
}

document.addEventListener('DOMContentLoaded', () => {
  Layout.render('My Orders');

  document.addEventListener('click', (e) => {
    if (e.target.closest('[data-action="fetch-orders"]')) {
      fetchUserOrders();
    }
    if (e.target.closest('[data-action="change-user"]')) {
      changeUser();
    }
  });

  document.getElementById('user-guid-input').addEventListener('keydown', (e) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      fetchUserOrders();
    }
  });
});
