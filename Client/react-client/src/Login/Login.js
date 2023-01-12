import './login.css';
import React from 'react';
import RequestManager from '../contracts/requests';
import CookieManager from '../utils/Cookie';

function Login() {

    let [login, setLogin] = React.useState('');
    let [password, setPassword] = React.useState('');

    async function handleLogin(e) {
        e.preventDefault();

        await RequestManager.loginUser(login, password)
            .then((res) => {
                if (res.ok){
                    return res.json();
                }
            })
            .then((data) => {
                CookieManager.SetCookie('token', data.token);
                CookieManager.SetCookie('swan-user', JSON.stringify(data.user));
                document.location.pathname = 'orders';
            });
    }

    return (
      <div className="ctr">
          <form onSubmit={handleLogin} className="form">
              <div className="header">ВОЙТИ</div>
              <div className="body">
                  <input type="text" autoComplete="off" placeholder="Логин" value={login} onChange={(e) => setLogin(e.target.value)}/>
                  <input type="password" placeholder="Пароль" value={password} onChange={(e) => setPassword(e.target.value)}/>
              </div>
              <div className="footer">
                  <input type="submit" className="btn btn-primary" value="Войти" />
              </div>
          </form>
      </div>
    );
}

export default Login;
