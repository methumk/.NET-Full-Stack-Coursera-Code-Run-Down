import Cookies from 'js-cookie'

export const cookieStrings = {
    loginObj : "userLoginObj"
}


export class CookieManager {
    constructor(cookieName) {
        this.cookieName = cookieName;
    }

    isEnabled()
    {
        const cookie = Cookies.get(this.cookieName);
        return cookie !== "" && cookie !== null && cookie !== undefined;
    }

    enable(setValue, isObj=true, expiresDay=1)
    {
        this.disable();
        const data = isObj ? JSON.stringify(setValue) : setValue;
        Cookies.set(this.cookieName, data, {expires: expiresDay});
    }

    disable()
    {
        Cookies.remove(this.cookieName);
    }

    get()
    {
        return Cookies.get(this.cookieName);
    }

    toJson()
    {
        if (this.isEnabled())
        {
            let dataString = this.get(); 
            return JSON.parse(dataString);
        }
        return null;
    }
}