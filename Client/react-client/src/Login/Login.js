import './login.css';
import React from 'react';

function Login() {

    let [login, setLogin] = React.useState('');
    let [password, setPassword] = React.useState('');

    async function handleLogin(e) {
        e.preventDefault();

        let loginModel = {
            login: login,
            password: password
        }
        let response = await fetch("/api/v1/login", {
            method: "POST",
            body: JSON.stringify(loginModel)
          });
        console.log(response.json());
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
