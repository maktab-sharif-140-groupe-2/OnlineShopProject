document.addEventListener('DOMContentLoaded', () => {
  Layout.render('Dashboard');

  const authStatus = document.getElementById('stat-status');
  if (ApiClient.isAuthenticated()) {
    authStatus.textContent = 'Authenticated';
    authStatus.style.color = 'var(--color-success)';
  } else {
    authStatus.textContent = 'Not Authenticated';
    authStatus.style.color = 'var(--color-danger)';
  }
});
