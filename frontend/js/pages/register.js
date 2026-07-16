document.addEventListener('DOMContentLoaded', () => {
  Layout.render('Create Account');

  document.getElementById('register-form').addEventListener('submit', async function(e) {
    e.preventDefault();
    const alertBox = document.getElementById('alert-box');
    const btn = document.getElementById('register-btn');
    Utils.clearAllErrors(this);

    const data = Utils.serializeForm(this);
    const errors = [];
    const fields = [
      { val: data.FullName, name: 'Full Name', id: 'fullName' },
      { val: data.Age, name: 'Age', id: 'age' },
      { val: data.Email, name: 'Email', id: 'email' },
      { val: data.PhoneNumber, name: 'Phone Number', id: 'phoneNumber' },
      { val: data.Password, name: 'Password', id: 'password' }
    ];
    fields.forEach(f => {
      const err = Utils.validateRequired(f.val, f.name);
      if (err) { Utils.showFieldError(document.getElementById(f.id), err); errors.push(err); }
    });
    if (errors.length) return;

    btn.disabled = true;
    btn.textContent = 'Creating account...';
    alertBox.innerHTML = '';

    try {
      await ApiClient.post('/api/Authentication/Register', data);
      showSuccess('Account created! Redirecting to login...');
      setTimeout(() => { window.location.href = '/pages/login.html'; }, 1500);
    } catch (err) {
      alertBox.innerHTML = '<div class="alert alert-error">' + Utils.escapeHtml(err.message) + '</div>';
      btn.disabled = false;
      btn.textContent = 'Create Account';
    }
  });
});
