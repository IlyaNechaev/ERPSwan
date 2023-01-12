import logo from './logo.svg';
import './App.css';
import Login from './Login/Login';
import Account from './Account'
import Orders from './pages/Orders';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='login' element={<Login/>} />
        <Route path='*' element={<Account/>}>
          <Route path='orders' element={<Orders/>}/>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
