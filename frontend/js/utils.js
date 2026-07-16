const Utils = {
  escapeHtml(str) {
    if (str === null || str === undefined) return '';
    const div = document.createElement('div');
    div.textContent = String(str);
    return div.innerHTML;
  },

  formatCurrency(amount) {
    return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(amount);
  },

  formatDate(dateStr) {
    if (!dateStr) return '-';
    return new Date(dateStr).toLocaleDateString('en-US', { year: 'numeric', month: 'short', day: 'numeric' });
  },

  formatDateTime(dateStr) {
    if (!dateStr) return '-';
    return new Date(dateStr).toLocaleString('en-US');
  },

  debounce(fn, delay = 300) {
    let timer;
    return function (...args) {
      clearTimeout(timer);
      timer = setTimeout(() => fn.apply(this, args), delay);
    };
  },

  getStatusBadgeClass(status) {
    const map = {
      'Pending': 'badge-warning',
      'Processing': 'badge-secondary',
      'Shipped': 'badge-secondary',
      'Delivered': 'badge-success',
      'Cancelled': 'badge-danger'
    };
    return map[status] || 'badge-secondary';
  },

  getInitials(name) {
    if (!name) return '?';
    return name.split(' ').map(w => w[0]).join('').toUpperCase().slice(0, 2);
  },

  serializeForm(form) {
    const data = {};
    const inputs = form.querySelectorAll('input, select, textarea');
    inputs.forEach(input => {
      const name = input.name;
      if (!name) return;
      if (input.type === 'checkbox') {
        data[name] = input.checked;
      } else if (input.type === 'number') {
        data[name] = input.value === '' ? '' : Number(input.value);
      } else {
        data[name] = input.value;
      }
    });
    return data;
  },

  validateRequired(value, fieldName) {
    if (value === '' || value === null || value === undefined) {
      return fieldName + ' is required';
    }
    return null;
  },

  validateRange(value, min, max, fieldName) {
    const num = Number(value);
    if (isNaN(num) || num < min || num > max) {
      return fieldName + ' must be between ' + min + ' and ' + max;
    }
    return null;
  },

  validateMaxLength(value, max, fieldName) {
    if (value && value.length > max) {
      return fieldName + ' must be at most ' + max + ' characters';
    }
    return null;
  },

  showFieldError(field, message) {
    this.clearFieldError(field);
    field.classList.add('is-invalid');
    const errorEl = document.createElement('div');
    errorEl.className = 'form-error';
    errorEl.textContent = message;
    field.parentNode.appendChild(errorEl);
  },

  clearFieldError(field) {
    field.classList.remove('is-invalid');
    const existing = field.parentNode.querySelector('.form-error');
    if (existing) existing.remove();
  },

  clearAllErrors(form) {
    form.querySelectorAll('.is-invalid').forEach(f => {
      f.classList.remove('is-invalid');
    });
    form.querySelectorAll('.form-error').forEach(e => e.remove());
  }
};
