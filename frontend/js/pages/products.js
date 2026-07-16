let currentPage = 1;

async function loadProducts(page) {
  currentPage = page;
  const container = document.getElementById('products-container');
  const pagContainer = document.getElementById('pagination-container');
  renderLoading(container);

  try {
    const data = await ApiClient.get('/api/Products', { pageNumber: page, size: CONFIG.PAGE_SIZE });
    if (!data.items || data.items.length === 0) {
      renderEmpty(container, 'No products found');
      pagContainer.innerHTML = '';
      return;
    }

    let html = '<div class="table-wrapper"><table><thead><tr><th>Name</th><th>Brand</th><th>Price</th><th>Actions</th></tr></thead><tbody>';
    data.items.forEach(item => {
      html += '<tr>';
      html += '<td>' + Utils.escapeHtml(item.name) + '</td>';
      html += '<td>' + Utils.escapeHtml(item.brandName) + '</td>';
      html += '<td>' + Utils.formatCurrency(item.price) + '</td>';
      html += '<td><div class="actions-cell">';
      html += '<button class="btn btn-sm btn-outline" data-action="open-price" data-name="' + Utils.escapeHtml(item.name) + '">Update Price</button>';
      html += '<button class="btn btn-sm btn-danger" data-action="open-delete" data-name="' + Utils.escapeHtml(item.name) + '">Delete</button>';
      html += '</div></td>';
      html += '</tr>';
    });
    html += '</tbody></table></div>';
    container.innerHTML = html;
    renderPagination(pagContainer, { pageNumber: data.pageNumber, pageSize: data.pageSize, totalPageCount: data.totalPageCount }, loadProducts);
  } catch (err) {
    renderError(container, err.message);
  }
}

function openPriceModal(name) {
  document.getElementById('price-product-name').textContent = name;
  document.getElementById('new-price').value = '';
  document.getElementById('price-alert').innerHTML = '';
  Utils.clearFieldError(document.getElementById('new-price'));
  showModal('modal-price');
}

function openDeleteModal(name) {
  document.getElementById('delete-product-name').textContent = name;
  document.getElementById('delete-alert').innerHTML = '';
  showModal('modal-delete');
}

async function savePrice() {
  const btn = document.getElementById('save-price-btn');
  const alertBox = document.getElementById('price-alert');
  const priceInput = document.getElementById('new-price');
  const guidInput = document.getElementById('price-guid');
  const val = priceInput.value;
  const guid = guidInput.value.trim();

  Utils.clearFieldError(priceInput);
  Utils.clearFieldError(guidInput);

  let hasError = false;
  if (!guid) {
    Utils.showFieldError(guidInput, 'Product ID is required');
    hasError = true;
  }
  if (!val || Number(val) <= 0) {
    Utils.showFieldError(priceInput, 'Price must be greater than 0');
    hasError = true;
  }
  if (hasError) return;

  btn.disabled = true;
  btn.textContent = 'Saving...';
  alertBox.innerHTML = '';

  try {
    await ApiClient.patch('/api/Products/UpdatePrice', {
      ProductId: guid,
      NewPrice: Number(val),
      ModifiederId: ApiClient.getUser()?.id || guid
    });
    hideModal('modal-price');
    showSuccess('Price updated successfully');
    loadProducts(currentPage);
  } catch (err) {
    alertBox.innerHTML = '<div class="alert alert-error">' + Utils.escapeHtml(err.message) + '</div>';
  }
  btn.disabled = false;
  btn.textContent = 'Save Price';
}

async function confirmDelete() {
  const btn = document.getElementById('confirm-delete-btn');
  const alertBox = document.getElementById('delete-alert');
  const guidInput = document.getElementById('delete-guid');
  const guid = guidInput.value.trim();

  Utils.clearFieldError(guidInput);
  if (!guid) {
    Utils.showFieldError(guidInput, 'Product ID is required');
    return;
  }

  btn.disabled = true;
  btn.textContent = 'Deleting...';
  alertBox.innerHTML = '';

  try {
    await ApiClient.delete('/api/Products/Remove', {
      productId: guid,
      deleterId: ApiClient.getUser()?.id || guid
    });
    hideModal('modal-delete');
    showSuccess('Product deleted successfully');
    loadProducts(currentPage);
  } catch (err) {
    alertBox.innerHTML = '<div class="alert alert-error">' + Utils.escapeHtml(err.message) + '</div>';
  }
  btn.disabled = false;
  btn.textContent = 'Delete';
}

document.addEventListener('DOMContentLoaded', () => {
  Layout.render('Products');

  document.addEventListener('click', (e) => {
    const action = e.target.closest('[data-action]');
    if (!action) return;

    switch (action.dataset.action) {
      case 'open-price':
        openPriceModal(action.dataset.name);
        break;
      case 'open-delete':
        openDeleteModal(action.dataset.name);
        break;
      case 'save-price':
        savePrice();
        break;
      case 'confirm-delete':
        confirmDelete();
        break;
    }
  });

  loadProducts(1);
});
