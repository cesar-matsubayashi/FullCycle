import Address from "./address";
import Customer from "./customer";
import Order from "./order";
import OrderItem from "./order_item";

describe("Order unit tests", () => {
    
    it("should throw error when id is empty", () => {
        expect(() => {
            let order = new Order("", "123", []);
        }).toThrow("Id is required");
    })

    it("should throw error when customerId is empty", () => {
        expect(() => {
            let order = new Order("1", "", []);
        }).toThrow("CustomerId is required");
    })

    it("should throw error when customerId is empty", () => {
        expect(() => {
            let order = new Order("1", "123", []);
        }).toThrow("Items are required");
    })

    it("should calculate total", () => {
        const item1 = new OrderItem("i1", "Item 1", 100, "p1", 2);
        const item2 = new OrderItem("i2", "Item 2", 200, "p2", 2);
        const order1 = new Order("o1", "c1", [item1]);

        let total = order1.total();
        expect(total).toBe(200);

        const order2 = new Order("o2", "c2", [item1, item2]);
        total = order2.total();
        expect(total).toBe(600);
    })

    it("should throw error if the item quantity is less then zero", () => {
        expect(() => {
            const item1 = new OrderItem("i1", "Item 1", 100, "p1", -1);
            const order1 = new Order("o1", "c1", [item1]);
        }).toThrow("Quantity must be greater then zero")
    })

});