import Cookie from 'js-cookie';

class CookieManager{
    static SetCookie = (name, usrin) => {
        Cookie.set(name, usrin, {
            expires: 14,
            secure: true,
            path:'/'
        });
    }

    static GetCookie = (name) => {
        return Cookie.get(name);
    }

    static RemoveCookie = (name) => {
        Cookie.remove(name);
    }
}

export default CookieManager;