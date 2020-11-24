//-----------------API CALLS---------------
const ContentType = "application/json; charset=utf-8";
const APIHost = "http://localhost:8000/";
const Header = "Access-Control - Allow - Origin: ";
const Items = APIHost + "Items/getList";
const RentedItems = APIHost + "RentedItems";
const RentItems = APIHost + "Rents/AddRents/";
const Users = APIHost + "Users/AdminPanel";
const Item = APIHost + "Items";
const AddToCart = APIHost + "Cart/Add";
const Token = APIHost + "Token";
const Cart = APIHost + "Cart/GetCart";
const RemoveFromCart = APIHost + "Cart/Remove";
const GetRentedItems = APIHost + "RentedItems/GetRentedItems";
const ReturnItems = APIHost + "RentedItems/ReturnItems";
const ResetPassword = APIHost + "Users/ResetPassword";
const ChangeUserRole = APIHost + "Users/ChangeUserRole";

//-----------------Another Variables---------------
const USER_EMAIL = { email: sessionStorage.getItem('email') };