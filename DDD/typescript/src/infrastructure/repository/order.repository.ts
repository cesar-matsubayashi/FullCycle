import Order from "../../domain/entity/order";
import OrderRepositoryInterface from "../../domain/repository/order-repository.interface";
import OrderItemModel from "../db/sequelize/model/order-item.model";
import OrderModel from "../db/sequelize/model/order.model";


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
    
    entity.items.forEach(item => {
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
    });
  

  }

  async find(id: string): Promise<Order> {
    throw Error("Method not implemented."); 
  }

  async findAll(): Promise<Order[]> {
    throw Error("Method not implemented."); 
  }
}