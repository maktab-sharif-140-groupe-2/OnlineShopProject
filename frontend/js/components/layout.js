const Layout = {
  nav: [
    { section: 'Main', items: [
      { href: '/index.html', label: 'Dashboard', icon: '📊' }
    ]},
    { section: 'Catalog', items: [
      { href: '/pages/products.html', label: 'Products', icon: '📦' },
      { href: '/pages/product-create.html', label: 'Add Product', icon: '➕' }
    ]},
    { section: 'Sales', items: [
      { href: '/pages/orders.html', label: 'All Orders', icon: '🧾' },
      { href: '/pages/my-orders.html', label: 'My Orders', icon: '👤' },
      { href: '/pages/order-create.html', label: 'Create Order', icon: '🛒' }
    ]},
    { section: 'Account', items: [
      { href: '/pages/login.html', label: 'Login', id: 'auth-nav' },
      { href: '/pages/register.html', label: 'Register', id: 'register-nav' }
    ]}
  ],

  render(pageTitle) {
    const currentPath = window.location.pathname;
    const currentPage = currentPath.split('/').pop() || 'index.html';

    const sidebarHtml = this.nav.map(group => {
      const itemsHtml = group.items.map(item => {
        const isActive = item.href.split('/').pop() === currentPage;
        const idAttr = item.id ? ` id="${item.id}"` : '';
        const activeClass = isActive ? ' class="active"' : '';
        return `<a href="${item.href}"${idAttr}${activeClass}>${item.label}</a>`;
      }).join('\n          ');
      return `<div class="nav-section">${group.section}</div>\n          ${itemsHtml}`;
    }).join('\n      ');

    const html = `
    <aside class="sidebar" id="sidebar">
      <div class="sidebar-header"><h1>ShopAdmin</h1></div>
      <nav class="sidebar-nav">
      ${sidebarHtml}
      </nav>
    </aside>
    <div class="overlay" id="overlay"></div>
    <main class="main-content">
      <header class="topbar">
        <div class="topbar-left">
          <button class="hamburger" aria-label="Toggle menu">&#9776;</button>
          <span class="topbar-title">${Utils.escapeHtml(pageTitle)}</span>
        </div>
        <div class="topbar-right">
          <div class="user-info hidden" id="user-section">
            <div class="user-avatar"></div>
            <span class="user-name"></span>
            <button class="btn btn-sm btn-outline" data-action="logout">Logout</button>
          </div>
        </div>
      </header>`;

    document.body.insertAdjacentHTML('afterbegin', html);

    document.addEventListener('click', (e) => {
      if (e.target.closest('[data-action="logout"]')) {
        logout();
      }
    });
  }
};
