import { Link, Navigate} from 'react-router-dom';
import {cookieStrings, CookieManager} from '../models/cookieModel.js';
import '../componentcss/Login.css'

function Logout()
{
    const cookieMan = new CookieManager(cookieStrings.loginObj);
    var loginObjJson = null;
    if (cookieMan.isEnabled())
    {
        loginObjJson = cookieMan.toJson();
    }else{
        // Redirect to login if login object is not saved to cookie
        Navigate('/login');
    }

    const handleLogout = () => {
        cookieMan.disable();
        console.log(`Is cookie disabled? ${cookieMan.isEnabled()}`)
    }

    return (
        <>
        <div className="login-layout">
            <h1>💪 Workout Tracker</h1>
            <h4>{loginObjJson === null ? 'Logout of Account' : `Logout of ${loginObjJson.username}'s Account`}</h4>
            <div className="login-container">
                <div className="login-input-container">
                    <Link to="/">
                        <button onClick={handleLogout}>Logout</button>
                    </Link>
                </div>
            </div> 
        </div>
        </>
    );
}

export default Logout;