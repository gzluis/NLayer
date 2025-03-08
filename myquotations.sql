USE master;

CREATE DATABASE myquotations

CREATE TABLE roles(
    rol_id INT IDENTITY(1,1) PRIMARY KEY,
    rol_name VARCHAR(60),
    rol_status BIT
);

CREATE TABLE users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    rol_id INT NOT NULL,
    user_name VARCHAR(60) NULL,
    user_nickname VARCHAR(60) NULL,
    user_password VARCHAR(100) NULL,
    user_status BIT,
    registration_date DATE,
    FOREIGN KEY (rol_id) REFERENCES roles(rol_id)
);

CREATE TABLE brands (
    brand_id INT IDENTITY(1,1) PRIMARY KEY,
    brand_name VARCHAR(150) NOT NULL
);

CREATE TABLE products(
    product_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    brand_id INT,
    supplier_name VARCHAR(255),
    product_code VARCHAR(100),
    product_name VARCHAR(255) NOT NULL,
    product_description VARCHAR(500),
    product_image VARCHAR(500),
    price_unit_purchase DECIMAL(18,2),
    price_unit_sale DECIMAL(18,2),
    stock INT,
    product_status BIT NOT NULL,
    FOREIGN KEY (brand_id) REFERENCES brands(brand_id)
);

CREATE TABLE quotations (
    quotation_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    client_name VARCHAR(255),
    client_phone VARCHAR(10),
    seller_name VARCHAR(255),
    user_id INT,
    payment_method_name VARCHAR(255),
    quotation_number BIGINT NOT NULL,
    validity_days INT,
    quotation_registration DATETIME, 
    total DECIMAL(18,2),
    quotation_status BIT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

CREATE TABLE quotation_details (
    quotation_detail_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    quotation_id BIGINT,
    product_id BIGINT,
    quantity INT,
    discount DECIMAL(18,2),
    subtotal DECIMAL(18,2),
    FOREIGN KEY (quotation_id) REFERENCES quotations(quotation_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);