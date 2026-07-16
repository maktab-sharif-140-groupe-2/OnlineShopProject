# Online Shop Frontend

Static HTML/CSS/JavaScript frontend for the OnlineShopProject .NET backend API.

## Requirements

- A static web server (e.g., VS Code Live Server, Python `http.server`, or `npx serve`)
- The .NET backend API running on `http://localhost:5000`

## Setup

1. Start the backend API
2. Serve the `frontend` directory with a static web server:
   ```
   cd frontend
   python -m http.server 8080
   ```
   Or use VS Code Live Server extension.
3. Open `http://localhost:8080` in your browser

## Configuration

Edit `js/config.js` to change the API base URL:
```javascript
const CONFIG = {
  API_BASE_URL: 'http://localhost:5000',
  // ...
};
```

## Pages

| Page | URL | Description |
|------|-----|-------------|
| Dashboard | `/index.html` | Overview and quick actions |
| Login | `/pages/login.html` | User authentication |
| Register | `/pages/register.html` | User registration |
| Products | `/pages/products.html` | List all products with pagination |
| Add Product | `/pages/product-create.html` | Create a new product |
| All Orders | `/pages/orders.html` | List all orders with pagination |
| My Orders | `/pages/my-orders.html` | List orders for a specific user |
| Create Order | `/pages/order-create.html` | Place a new order |

## API Endpoints Used

- `POST /api/Authentication/Login` - Login
- `POST /api/Authentication/Register` - Register
- `GET /api/Products` - List products (paginated)
- `POST /api/Products` - Create product
- `DELETE /api/Products/Remove` - Delete product
- `PATCH /api/Products/UpdatePrice` - Update product price
- `PATCH /api/Products/UpdateStock` - Update product stock
- `POST /api/Order` - Create order
- `GET /api/Order/Orders` - List all orders (paginated)
- `GET /api/Order/Orders{userId}` - List user orders (paginated)

## Notes

- JWT tokens are stored in `localStorage`
- Authentication is required for Products and Orders endpoints
- The Product API does not return product IDs in the list response, so update/delete operations require manual ID input
- All pages are responsive (mobile, tablet, desktop)
- No external dependencies or build tools required
