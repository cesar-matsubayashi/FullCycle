import Address from "../../domain/entity/address";
import Customer from "../../domain/entity/customer";
import Order from "../../domain/entity/order";
import OrderItem from "../../domain/entity/order_item";
import OrderRepositoryInterface from "../../domain/repository/order-repository.interface";
import CustomerModel from "../db/sequelize/model/customer.model";
import OrderItemModel from "../db/sequelize/model/order-item.model";
import OrderModel from "../db/sequelize/model/order.model";
import CustomerRepository from "./customer.repository";


export default class OrderRepository implements OrderRepositoryInterface {
  async create(entity: Order): Promise<void> {
    await OrderModel.create({
      id: entity.id,
      customer_id: entity.customerId,
      total: entity.total(),
      items: entity.items.map((item) => ({
        id: item.id,
        name: item.name,
        price: item.price,
        product_id: item.productId,
        quantity: item.quantity,
      }))
    },
      {
        include: [{ model: OrderItemModel }]
      }
    );
  }

  async update(entity: Order): Promise<void> {
    await OrderModel.update({
      customer_id: entity.customerId,
      total: entity.total(),
    },
      {
        where: {
          id: entity.id
        }
      },
    );
    
   await Promise.all(entity.items.map(async item => {
       OrderItemModel.update({
        name: item.name,
        price: item.price,
        product_id: item.productId,
        quantity: item.quantity,
      },
      {
        where: {
          id: item.id
        }
      })
    })); 
  

  }

  async find(id: string): Promise<Order> {
    let orderModel;
    try {
      orderModel = await OrderModel.findOne({
        where: {
          id,
        },
        include: ["items"],
        rejectOnEmpty: true,
      });
    } catch (error) {
      throw new Error("Order not found");
    }

    const orderItem: OrderItem[] = []

    orderModel.items.forEach((item) => {
      orderItem.push(
        new OrderItem(
          item.id, 
          item.name, 
          item.price, 
          item.product_id, 
          item.quantity))
    });

    const order = new Order(orderModel.id, orderModel.customer_id, orderItem)
    return order
  }

  async findAll(): Promise<Order[]> {
    const orderModels = await OrderModel.findAll({include: ["items"]});
    const orders = orderModels.map((orderModels) => {
      let orderItems: OrderItem[] = []
      orderModels.items.forEach((item) => {
        orderItems.push(
          new OrderItem(
            item.id, 
            item.name, 
            item.price, 
            item.product_id, 
            item.quantity))
      });
  
      let order = new Order(orderModels.id, orderModels.customer_id, orderItems)
      return order;
    });

    return orders
  }
}