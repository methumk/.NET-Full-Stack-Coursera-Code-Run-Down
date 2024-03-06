import { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import {cookieStrings, CookieManager} from '../models/cookieModel.js';
import '../componentcss/Login.css'

function capitalizeFirstLetter(str) 
{
    return str.charAt(0).toUpperCase() + str.slice(1);
}

function Login()
{
    const [emailError, setEmailError] = useState(false);
    const [passError, setPassError] = useState(false);
    const [incorrectLogin, setIncorrectLogin] = useState(false);

    const navigate = useNavigate();
    const cookieMan = new CookieManager(cookieStrings.loginObj);
    if (cookieMan.isEnabled())
    {
        // already logged in
        navigate('/logout')
    }

    async function ValidateLogin(e) {
        // Prevent the browser from reloading the page
        e.preventDefault();

        // Read the form data
        const form = e.target;
        const formData = new FormData(form);
        const loginReq = {email: formData.get("email").trim(), password : formData.get("password").trim()};
        console.log(loginReq)

        loginReq.email === "" ? setEmailError(true) : setEmailError(false);
        loginReq.password === "" ? setPassError(true) : setPassError(false);
        if (loginReq.email !== "" && loginReq.password !== "")
        {
            var tokenInfo = await FetchLoginInfo(loginReq);
            if (tokenInfo !== null)
            {
                console.log(tokenInfo);
                tokenInfo.username = capitalizeFirstLetter(tokenInfo.username);
                cookieMan.enable(tokenInfo);
                navigate('/');
            }
        }
    }

    return (
        <>
        <div className="login-layout">
            <h1>💪 Workout Tracker</h1>
            <h4>Login to view workouts</h4>
            <div className="login-container">
                <form method="post" onSubmit={ValidateLogin}>
                    <div className="login-input-container">
                        <div className="login-label-input">
                            <label className='login-label'>Email: </label>
                            <input className='login-input' type="email" name="email" placeholder='email' required></input>
                        </div>
                        {emailError && <p style={{ color: 'red' }}><b>Email cannot be empty</b></p>}
                    </div>
                    <div className="login-input-container">
                        <div className="login-label-input">
                            <label className='login-label'>Password: </label>
                            <input className='login-input' type="password" name="password" placeholder='password' required></input>
                        </div>
                        {passError && <p style={{ color: 'red' }}><b>Password cannot be empty</b></p>}
                        {incorrectLogin && !emailError && !passError && <p style={{ color: 'red' }}><b>Incorrect login info</b></p>}
                    </div>
                    <div className="login-input-container">
                        <button type="submit">Login</button>
                    </div>
                </form>
            </div> 
            <div className="login-register">
                <Link to="/register">No account? Register here</Link>
            </div>
        </div>
        </>
    );

    async function FetchLoginInfo(loginObj)
    {
        const resp = await fetch("http://localhost:5286/User/Login", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginObj)
        });
        
        if (resp.ok)
        {
            setIncorrectLogin(false);
            const respJson = await resp.json();
            return respJson;
        }

        setIncorrectLogin(true);
        return null;
    }
}

export default Login