# Controller API Map

## Controllers Discovered

### 1. AuthenticationController
**Base Route:** `api/Authentication`
**Controller-level Auth:** None (public)

| Method | Endpoint | Auth Policy | Request Body | Response | Status Codes |
|--------|----------|-------------|--------------|----------|--------------|
| POST | `api/Authentication/Register` | None | `RegisterCommand` (form-data: FullName, Age, Email, PhoneNumber, Password) | 200 OK (RegisterCommand echo) | 200, 400 |
| POST | `api/Authentication/Login` | None | `LoginCommand` (form-data: UserName, Password) | 200 OK (`TokenLoginQuery { AccessToken, ExpiresIn }`) | 200, 401 |

### 2. ProductsController
**Base Route:** `api/Products`
**Controller-level Auth:** `[Authorize]` (JWT required)

| Method | Endpoint | Auth Policy | Request | Response | Status Codes |
|--------|----------|-------------|---------|----------|--------------|
| POST | `api/Products` | CanCreateProduct | Body: `{ Name, BrandName, Stock, Price }` | 201 Created | 201, 400, 401, 403 |
| GET | `api/Products` | ApplicationLogic | Query: `pageNumber=1&size=10` | `Pagination<ProductQuery> { Items: [{ Name, BrandName, Price }], PageNumber, PageSize, TotalPageCount }` | 200, 401, 403 |
| DELETE | `api/Products/Remove` | CanDeleteProduct | Query: `productId={Guid}&deleterId={Guid}` | `bool` | 200, 401, 403 |
| PATCH | `api/Products/UpdatePrice` | CanChangeProduct | Body: `{ ProductId, NewPrice, ModifiederId }` | `bool` | 200, 400, 401, 403 |
| PATCH | `api/Products/UpdateStock` | CanChangeProduct | Body: `{ productId, stock, modifierId }` | `bool` | 200, 400, 401, 403 |

### 3. OrderController
**Base Route:** `api/Order`
**Controller-level Auth:** None (individual actions require auth)

| Method | Endpoint | Auth Policy | Request | Response | Status Codes |
|--------|----------|-------------|---------|----------|--------------|
| POST | `api/Order` | ApplicationLogic | Body: `{ UserId, ProductId, Quantity }` | 200 OK (`true`) | 200, 400, 401, 403 |
| GET | `api/Order/Orders` | CanReadOrders | Query: `page={int}&pageSize={int}` | `Pagination<OrderQuery>` | 200, 401, 403 |
| GET | `api/Order/Orders{userId}` | CanReadUserOrders | Route: `userId` (Guid), Query: `page={int}&pageSize={int}` | `Pagination<OrderQuery>` | 200, 401, 403 |

## Authentication
- **Type:** JWT Bearer
- **Header:** `Authorization: Bearer {token}`
- **Login Response:** `{ "accessToken": "string", "expiresIn": number }`

## Pagination Response Schema
```json
{
  "items": [],
  "pageNumber": 1,
  "pageSize": 10,
  "totalPageCount": 5
}
```

## OrderQuery Schema
```json
{
  "items": [{ "name": "", "brandName": "", "price": 0 }],
  "ordererName": "",
  "totalPrice": 0,
  "deliveryDate": "2024-01-01",
  "status": ""
}
```

## ProductQuery Schema
```json
{
  "name": "",
  "brandName": "",
  "price": 0
}
```

## Ambiguities
- `CanChangeProduct` policy is referenced in ProductsController but `CanUpdateProduct` is defined in Program.cs (possible mismatch)
- OrderController route `Orders{userId:Guid}` may produce URLs like `api/Order/Orders{guid}` without a separator
- No GET-by-id endpoint exists for products or orders
- ProductQuery does not include an `Id` field, making individual product operations difficult from the UI
