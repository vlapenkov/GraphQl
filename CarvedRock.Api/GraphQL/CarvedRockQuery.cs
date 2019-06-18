using CarvedRock.Api.Data;
using CarvedRock.Api.GraphQL.Types;
using CarvedRock.Api.Repositories;
using GraphQL.Types;
using System;
using System.Linq;

namespace CarvedRock.Api.GraphQL
{
    public class CarvedRockQuery: ObjectGraphType
    {
        public CarvedRockQuery(ProductRepository productRepository , CarvedRockDbContext db)
        {
            Field<ListGraphType<ProductType>>(
                "products", 
                resolve: context => productRepository.GetAll()
            );

            Field<ProductType>(
                "product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> {Name = "id"}
                ),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return productRepository.GetOne(id);
                }
            );

            Field<ListGraphType<ProductType>>(
                "productsnew",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "first" },
                    new QueryArgument<StringGraphType> { Name = "orderby" }
                ),
                resolve: context =>
                {
                    var first = context.GetArgument<int>("first");
                    var order_by = context.GetArgument<string>("orderby");

                    var query = db.Products.Take(first);
                    if (order_by=="id")
                        query= query.OrderBy(order=> order.Id);
                    else
                        query = query.OrderBy(order => order.Name);

                    return query;

                    // return productRepository.GetOne(id);
                }
            );

            Field<ListGraphType<NewProductType>>(
                "anytype",
                resolve: context =>
                {
                    return db.Products.Select(product => new NewProduct { Id = product.Id, Name = product.Name } );
                }
            );
        }
    }
}
