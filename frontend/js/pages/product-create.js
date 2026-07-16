document.addEventListener('DOMContentLoaded', () => {
  Layout.render('Add Product');

  document.getElementById('product-form').addEventListener('submit', async function(e) {
    e.preventDefault();
    const alertBox = document.getElementById('form-alert');
    const btn = document.getElementById('submit-btn');
    Utils.clearAllErrors(this);

    const data = Utils.serializeForm(this);
    const errors = [];

    const nameErr = Utils.validateRequired(data.Name, 'Product Name') || Utils.validateMaxLength(data.Name, 100, 'Product Name');
    if (nameErr) { Utils.showFieldError(document.getElementById('name'), nameErr); errors.push(nameErr); }

    const brandErr = Utils.validateRequired(data.BrandName, 'Brand Name') || Utils.validateMaxLength(data.BrandName, 100, 'Brand Name');
    if (brandErr) { Utils.showFieldError(document.getElementById('brandName'), brandErr); errors.push(brandErr); }

    const stockErr = Utils.validateRequired(data.Stock, 'Stock') || Utils.validateRange(data.Stock, 1, 1000000, 'Stock');
    if (stockErr) { Utils.showFieldError(document.getElementById('stock'), stockErr); errors.push(stockErr); }

    const priceErr = Utils.validateRequired(data.Price, 'Price') || Utils.validateRange(data.Price, 0.01, 2147483647, 'Price');
    if (priceErr) { Utils.showFieldError(document.getElementById('price'), priceErr); errors.push(priceErr); }

    if (errors.length) return;

    btn.disabled = true;
    btn.textContent = 'Creating...';
    alertBox.innerHTML = '';

    try {
      await ApiClient.post('/api/Products', data);
      showSuccess('Product created successfully!');
      setTimeout(() => { window.location.href = '/pages/products.html'; }, 800);
    } catch (err) {
      alertBox.innerHTML = '<div class="alert alert-error">' + Utils.escapeHtml(err.message) + '</div>';
      btn.disabled = false;
      btn.textContent = 'Create Product';
    }
  });
});
