
Позволяет задать запрос с параметрами (productsnew) и запрос любого типа (anytype )
{
  productsnew (first:5,orderby:"name")
  {
    name,
    description,    
    id
  },
anytype 
 {
    name,
   
    id
  }  
}
