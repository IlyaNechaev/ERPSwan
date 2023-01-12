import logo from './logo.svg';
import './App.css';
import Login from './Login/Login';
import Account from './Account'
import Orders from './pages/Orders';
import Order from './pages/Views/Order';
import CookieManager from './utils/Cookie';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import OrderMaster from './pages/Masters/OrderMaster';

function App() {
  let jwt = CookieManager.GetCookie('token');

  if (jwt == null){
    return (
      <BrowserRouter>
        <Routes>
            <Route path='login' element={<Login/>}/>
            <Route path='*' element={<Navigate to='login'/>} />
        </Routes>
      </BrowserRouter>);
  }
  
  return (
    <BrowserRouter>
      <Routes>
        <Route path='*' element={<Navigate to='swan/orders'/>}/>
        <Route path='swan' element={<Account/>}>
          <Route path='orders' element={<Orders/>}/>
          <Route path='order' element={<Order />}/>
          <Route path='new-order' element={<OrderMaster />}/>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
