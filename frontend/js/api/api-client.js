const ApiClient = {
  getToken() {
    return localStorage.getItem(CONFIG.TOKEN_KEY);
  },

  setToken(token) {
    localStorage.setItem(CONFIG.TOKEN_KEY, token);
  },

  removeToken() {
    localStorage.removeItem(CONFIG.TOKEN_KEY);
    localStorage.removeItem(CONFIG.USER_KEY);
  },

  isAuthenticated() {
    return !!this.getToken();
  },

  getUser() {
    const raw = localStorage.getItem(CONFIG.USER_KEY);
    return raw ? JSON.parse(raw) : null;
  },

  setUser(user) {
    localStorage.setItem(CONFIG.USER_KEY, JSON.stringify(user));
  },

  buildHeaders(isFormData = false) {
    const headers = {};
    if (!isFormData) {
      headers['Content-Type'] = 'application/json';
    }
    const token = this.getToken();
    if (token) {
      headers['Authorization'] = 'Bearer ' + token;
    }
    return headers;
  },

  buildQueryString(params) {
    const parts = [];
    for (const [key, value] of Object.entries(params)) {
      if (value !== undefined && value !== null && value !== '') {
        parts.push(encodeURIComponent(key) + '=' + encodeURIComponent(value));
      }
    }
    return parts.length ? '?' + parts.join('&') : '';
  },

  async request(method, url, body = null, options = {}) {
    const { isFormData = false, params = {} } = options;
    const queryString = this.buildQueryString(params);
    const fullUrl = CONFIG.API_BASE_URL + url + queryString;

    const config = {
      method,
      headers: this.buildHeaders(isFormData)
    };

    if (body) {
      config.body = isFormData ? body : JSON.stringify(body);
    }

    try {
      const response = await fetch(fullUrl, config);

      if (response.status === 401) {
        this.removeToken();
        window.location.href = '/pages/login.html';
        throw new ApiError('Unauthorized', 401);
      }

      if (response.status === 403) {
        throw new ApiError('You do not have permission to perform this action', 403);
      }

      if (response.status === 204) {
        return null;
      }

      if (!response.ok) {
        let errorMessage = 'Request failed';
        try {
          const errorData = await response.json();
          if (errorData.message) errorMessage = errorData.message;
          if (typeof errorData === 'string') errorMessage = errorData;
        } catch {
          errorMessage = response.statusText || 'Request failed';
        }
        throw new ApiError(errorMessage, response.status);
      }

      const text = await response.text();
      if (!text) return null;
      return JSON.parse(text);
    } catch (error) {
      if (error instanceof ApiError) throw error;
      throw new ApiError('Network error. Please check your connection.', 0);
    }
  },

  get(url, params = {}) {
    return this.request('GET', url, null, { params });
  },

  post(url, body) {
    return this.request('POST', url, body);
  },

  postFormData(url, formData) {
    return this.request('POST', url, formData, { isFormData: true });
  },

  put(url, body) {
    return this.request('PUT', url, body);
  },

  patch(url, body) {
    return this.request('PATCH', url, body);
  },

  delete(url, params = {}) {
    return this.request('DELETE', url, null, { params });
  }
};

class ApiError extends Error {
  constructor(message, status) {
    super(message);
    this.name = 'ApiError';
    this.status = status;
  }
}
