document.addEventListener('DOMContentLoaded', () => {
  Layout.render('Sign In');

  document.getElementById('login-form').addEventListener('submit', async function(e) {
    e.preventDefault();
    const alertBox = document.getElementById('alert-box');
    const btn = document.getElementById('login-btn');
    Utils.clearAllErrors(this);

    const data = Utils.serializeForm(this);
    const errors = [];
    const err1 = Utils.validateRequired(data.UserName, 'Username');
    const err2 = Utils.validateRequired(data.Password, 'Password');
    if (err1) { Utils.showFieldError(document.getElementById('username'), err1); errors.push(err1); }
    if (err2) { Utils.showFieldError(document.getElementById('password'), err2); errors.push(err2); }
    if (errors.length) return;

    btn.disabled = true;
    btn.textContent = 'Signing in...';
    alertBox.innerHTML = '';

    try {
      const result = await ApiClient.post('/api/Authentication/Login', data);
      ApiClient.setToken(result.accessToken);
      ApiClient.setUser({ name: data.UserName, expires: result.expiresIn });
      showSuccess('Login successful!');
      setTimeout(() => { window.location.href = '/index.html'; }, 500);
    } catch (err) {
      alertBox.innerHTML = '<div class="alert alert-error">' + Utils.escapeHtml(err.message) + '</div>';
      btn.disabled = false;
      btn.textContent = 'Sign In';
    }
  });
});
