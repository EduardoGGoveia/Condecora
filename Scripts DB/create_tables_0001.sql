CREATE TABLE category_badge (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR(150) NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    
    CONSTRAINT uq_category_badge_name UNIQUE (name),
    CONSTRAINT chk_category_badge_name_not_empty CHECK (TRIM(name) <> '')
);

CREATE TABLE badge (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    category_id INT NOT NULL,
    name VARCHAR(150) NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    
    CONSTRAINT fk_badge_category FOREIGN KEY (category_id) 
        REFERENCES category_badge(id) ON DELETE RESTRICT,
    CONSTRAINT uq_badge_name UNIQUE (name),
    CONSTRAINT chk_badge_name_not_empty CHECK (TRIM(name) <> '')
);

CREATE TABLE badge_stock (
    badge_id INT PRIMARY KEY,
    quantity INT NOT NULL DEFAULT 0,
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    
    CONSTRAINT fk_stock_badge FOREIGN KEY (badge_id)
        REFERENCES badge(id) ON DELETE CASCADE,
    CONSTRAINT chk_quantity_stock_non_negative CHECK (quantity >= 0)
);

CREATE TABLE stock_movement (
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    badge_id INT NOT NULL,
    quantity INT NOT NULL,
    description VARCHAR(250),
    scheduled_for TIMESTAMPTZ,
    type_movement VARCHAR(10) NOT NULL,
    status VARCHAR(20) NOT NULL DEFAULT 'PENDING',
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    
    CONSTRAINT fk_movement_badge FOREIGN KEY (badge_id)
        REFERENCES badge(id) ON DELETE RESTRICT,
    CONSTRAINT chk_movement_quantity_positive CHECK (quantity > 0),
    CONSTRAINT chk_type_movement_valid CHECK (type_movement IN ('IN', 'OUT')),
    CONSTRAINT chk_movement_status_valid CHECK (status IN ('PENDING', 'COMPLETED', 'CANCELED'))
);