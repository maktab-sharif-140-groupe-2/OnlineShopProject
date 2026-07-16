function showNotification(message, type) {
  const existing = document.querySelector('.notification-toast');
  if (existing) existing.remove();

  const toast = document.createElement('div');
  toast.className = 'notification-toast alert alert-' + (type || 'success');
  toast.style.cssText = 'position:fixed;top:1rem;right:1rem;z-index:9999;max-width:400px;animation:slideIn .3s ease';
  toast.textContent = message;
  document.body.appendChild(toast);

  setTimeout(() => {
    toast.style.opacity = '0';
    toast.style.transition = 'opacity .3s';
    setTimeout(() => toast.remove(), 300);
  }, 3500);
}

function showError(message) {
  showNotification(message, 'error');
}

function showSuccess(message) {
  showNotification(message, 'success');
}

function renderPagination(container, pagination, onPageChange) {
  container.innerHTML = '';
  if (!pagination || pagination.totalPageCount <= 1) return;

  const prev = document.createElement('button');
  prev.textContent = 'Prev';
  prev.disabled = pagination.pageNumber <= 1;
  prev.onclick = () => onPageChange(pagination.pageNumber - 1);
  container.appendChild(prev);

  for (let i = 1; i <= pagination.totalPageCount; i++) {
    if (pagination.totalPageCount > 7 && Math.abs(i - pagination.pageNumber) > 2 && i !== 1 && i !== pagination.totalPageCount) {
      if (i === pagination.pageNumber - 3 || i === pagination.pageNumber + 3) {
        const dots = document.createElement('span');
        dots.textContent = '...';
        dots.className = 'text-muted';
        dots.style.padding = '0 .5rem';
        container.appendChild(dots);
      }
      continue;
    }
    const btn = document.createElement('button');
    btn.textContent = i;
    btn.className = i === pagination.pageNumber ? 'active' : '';
    btn.onclick = () => onPageChange(i);
    container.appendChild(btn);
  }

  const next = document.createElement('button');
  next.textContent = 'Next';
  next.disabled = pagination.pageNumber >= pagination.totalPageCount;
  next.onclick = () => onPageChange(pagination.pageNumber + 1);
  container.appendChild(next);
}

function renderLoading(container) {
  container.innerHTML = '<div class="loading"><div class="spinner"></div><span>Loading...</span></div>';
}

function renderError(container, message) {
  container.innerHTML = '<div class="alert alert-error">' + Utils.escapeHtml(message) + '</div>';
}

function renderEmpty(container, message) {
  container.innerHTML = '<div class="empty-state"><p>' + Utils.escapeHtml(message || 'No data found') + '</p></div>';
}

function showModal(modalId) {
  document.getElementById(modalId).classList.add('active');
}

function hideModal(modalId) {
  document.getElementById(modalId).classList.remove('active');
}

function setupSidebar() {
  const sidebar = document.querySelector('.sidebar');
  const overlay = document.querySelector('.overlay');
  const hamburger = document.querySelector('.hamburger');

  if (hamburger) {
    hamburger.addEventListener('click', () => {
      sidebar.classList.toggle('open');
      overlay.classList.toggle('active');
    });
  }

  if (overlay) {
    overlay.addEventListener('click', () => {
      sidebar.classList.remove('open');
      overlay.classList.remove('active');
    });
  }
}

function updateAuthUI() {
  const authNav = document.getElementById('auth-nav');
  const registerNav = document.getElementById('register-nav');
  const userSection = document.getElementById('user-section');

  if (ApiClient.isAuthenticated()) {
    if (authNav) authNav.classList.add('hidden');
    if (registerNav) registerNav.classList.add('hidden');
    if (userSection) {
      userSection.classList.remove('hidden');
      const user = ApiClient.getUser();
      const nameEl = userSection.querySelector('.user-name');
      const avatarEl = userSection.querySelector('.user-avatar');
      if (nameEl && user) nameEl.textContent = user.name || user.email || 'User';
      if (avatarEl && user) avatarEl.textContent = Utils.getInitials(user.name || user.email);
    }
  } else {
    if (authNav) authNav.classList.remove('hidden');
    if (registerNav) registerNav.classList.remove('hidden');
    if (userSection) userSection.classList.add('hidden');
  }
}

function logout() {
  ApiClient.removeToken();
  window.location.href = '/pages/login.html';
}

function requireAuth() {
  if (!ApiClient.isAuthenticated()) {
    window.location.href = '/pages/login.html';
    return false;
  }
  return true;
}

function setupModalClose() {
  document.addEventListener('click', (e) => {
    const overlay = e.target.closest('.modal-overlay');
    if (overlay && e.target === overlay) {
      overlay.classList.remove('active');
    }
    const closeBtn = e.target.closest('[data-close-modal]');
    if (closeBtn) {
      const modal = closeBtn.closest('.modal-overlay');
      if (modal) modal.classList.remove('active');
    }
  });

  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') {
      document.querySelectorAll('.modal-overlay.active').forEach(m => m.classList.remove('active'));
    }
  });
}

document.addEventListener('DOMContentLoaded', () => {
  setupSidebar();
  updateAuthUI();
  setupModalClose();
});
