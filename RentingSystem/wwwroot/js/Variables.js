//-----------------API CALLS---------------
const ContentType = "application/json; charset=utf-8";
const APIHost = "http://localhost:8000/";
const Header = "Access-Control - Allow - Origin: ";
const Items = APIHost + "Items/getList";
const RentedItems = APIHost + "RentedItems";
const RentItems = APIHost + "Rents/AddRents/";
const User = APIHost + "Users";
const Item = APIHost + "Items";
const AddToCart = APIHost + "Cart/Add";
const Token = APIHost + "Token";
const Cart = APIHost + "Cart/GetCart";
const RemoveFromCart = APIHost + "Cart/Remove";
const GetRentedItems = APIHost + "RentedItems/GetRentedItems";
const ReturnItem = APIHost + "RentedItems/ReturnItem";

const USER_EMAIL = { email: sessionStorage.getItem('email') };