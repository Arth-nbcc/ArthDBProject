create table order_details
(
order_detailsID int primary key identity,
invoice_id int foreign key references order_master(invoice_id),
item_name varchar(50) not null,
unit_price float not null,
discount_peritem float not null,
quantity float not null,
subtotal float not null,
tax float not null,
totalcost float not null
);