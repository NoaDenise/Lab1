# API Endpoints

## Customers

### 1. Get all customers
- **Endpoint**: `GET /api/Customer`
- **Example response**:
    ```json
    [
      {
        "customerId": 1,
        "name": "Alice Smith",
        "email": "alice.smith@example.com"
      },
      {
        "customerId": 2,
        "name": "Bob Johnson",
        "email": "bob.johnson@example.com"
      }
    ]
    ```

### 2. Get customer by ID
- **Endpoint**: `GET /api/Customer/{customerId}`
- **Example response**:
    ```json
    {
      "customerId": 1,
      "name": "Alice Smith",
      "email": "alice.smith@example.com"
    }
    ```

### 3. Create a new customer
- **Endpoint**: `POST /api/Customer`
- **Example request**:
    ```json
    {
      "name": "Charlie Brown",
      "email": "charlie.brown@example.com",
      "phone": "555-555-5555"
    }
    ```
- **Example response**:
    ```json
    {
      "customerId": 3,
      "name": "Charlie Brown",
      "email": "charlie.brown@example.com"
    }
    ```

### 4. Update a customer
- **Endpoint**: `PUT /api/Customer/{customerId}`
- **Example request**:
    ```json
    {
      "customerId": 3,
      "name": "Charlie B.",
      "email": "charlie.b@example.com",
      "phone": "555-555-1234"
    }
    ```

### 5. Delete a customer
- **Endpoint**: `DELETE /api/Customer/{customerId}`


## Tables

### 1. Get all tables
- **Endpoint**: `GET /api/Table`
- **Example response**:
    ```json
    [
      {
        "tableId": 1,
        "tableNumber": 1,
        "numberOfSeats": 4
      },
      {
        "tableId": 2,
        "tableNumber": 2,
        "numberOfSeats": 2
      }
    ]
    ```

### 2. Create a new table
- **Endpoint**: `POST /api/Table`
- **Example request**:
    ```json
    {
      "tableNumber": 3,
      "numberOfSeats": 6
    }
    ```
- **Example response**:
    ```json
    {
      "tableId": 3,
      "tableNumber": 3,
      "numberOfSeats": 6
    }
    ```

### 3. Delete a table
- **Endpoint**: `DELETE /api/Table/{tableId}`


## Reservations

### 1. Get all reservations
- **Endpoint**: `GET /api/Reservation`
- **Example response**:
    ```json
    [
      {
        "reservationId": 1,
        "customerId": 1,
        "tableId": 1,
        "reservationDate": "2024-09-18T18:30:00",
        "numberOfGuests": 2
      }
    ]
    ```

### 2. Create a new reservation
- **Endpoint**: `POST /api/Reservation`
- **Example request**:
    ```json
    {
      "customerId": 1,
      "tableId": 1,
      "reservationDate": "2024-09-18T18:30:00",
      "numberOfGuests": 2
    }
    ```
- **Example response**:
    ```json
    {
      "reservationId": 1,
      "customerId": 1,
      "tableId": 1,
      "reservationDate": "2024-09-18T18:30:00",
      "numberOfGuests": 2
    }
    ```


## Menu Items

### 1. Get all menu items
- **Endpoint**: `GET /api/MenuItem`
- **Example response**:
    ```json
    [
      {
        "menuItemId": 1,
        "name": "Pasta",
        "price": 120,
        "isAvailable": true
      },
      {
        "menuItemId": 2,
        "name": "Pizza",
        "price": 100,
        "isAvailable": false
      }
    ]
    ```

### 2. Create a new menu item
- **Endpoint**: `POST /api/MenuItem`
- **Example request**:
    ```json
    {
      "name": "Sallad",
      "price": 80,
      "isAvailable": true
    }
    ```
- **Example response**:
    ```json
    {
      "menuItemId": 3,
      "name": "Sallad",
      "price": 80,
      "isAvailable": true
    }
    ```

---

