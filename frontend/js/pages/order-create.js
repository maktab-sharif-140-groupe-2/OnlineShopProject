document.addEventListener('DOMContentLoaded', () => {
  Layout.render('Create Order');

  document.getElementById('order-form').addEventListener('submit', async function(e) {
    e.preventDefault();
    const alertBox = document.getElementById('form-alert');
    const btn = document.getElementById('submit-btn');
    Utils.clearAllErrors(this);

    const data = Utils.serializeForm(this);
    const errors = [];

    const userErr = Utils.validateRequired(data.UserId, 'User ID');
    if (userErr) { Utils.showFieldError(document.getElementById('userId'), userErr); errors.push(userErr); }

    const prodErr = Utils.validateRequired(data.ProductId, 'Product ID');
    if (prodErr) { Utils.showFieldError(document.getElementById('productId'), prodErr); errors.push(prodErr); }

    const qtyErr = Utils.validateRequired(data.Quantity, 'Quantity') || Utils.validateRange(data.Quantity, 1, 1000000, 'Quantity');
    if (qtyErr) { Utils.showFieldError(document.getElementById('quantity'), qtyErr); errors.push(qtyErr); }

    if (errors.length) return;

    btn.disabled = true;
    btn.textContent = 'Placing order...';
    alertBox.innerHTML = '';

    try {
      await ApiClient.post('/api/Order', data);
      showSuccess('Order placed successfully!');
      setTimeout(() => { window.location.href = '/pages/orders.html'; }, 800);
    } catch (err) {
      alertBox.innerHTML = '<div class="alert alert-error">' + Utils.escapeHtml(err.message) + '</div>';
      btn.disabled = false;
      btn.textContent = 'Place Order';
    }
  });
});
