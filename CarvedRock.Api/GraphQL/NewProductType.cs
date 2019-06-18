using GraphQL.Types;

namespace CarvedRock.Api.GraphQL
{
    public class NewProductType : ObjectGraphType<NewProduct>
    {
        public NewProductType()
        {
            Field(t => t.Id);
            Field(t => t.Name);
        }
    }
}