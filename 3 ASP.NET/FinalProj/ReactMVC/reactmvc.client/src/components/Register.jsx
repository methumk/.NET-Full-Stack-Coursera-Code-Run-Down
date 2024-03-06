import { useState } from 'react';
import { Link, useNavigate  } from 'react-router-dom';
import '../componentcss/Login.css'


function Register() {
    const navigate = useNavigate();
    const [registerError, setRegisterError] = useState(false);
    const [infoError, setInfoError] = useState({
        emailError: false,
        usernameError: false,
        passwordError: false
    });

    async function ValidateRegister(e) {
        // Prevent the browser from reloading the page
        e.preventDefault();

        // Read the form data
        const form = e.target;
        const formData = new FormData(form);
        const loginReq = { email: formData.get("email").trim(), username: formData.get("username").trim(), password : formData.get("password").trim() };
        console.log(loginReq)

        loginReq.email === "" ? 
            setInfoError(infoError => ({...infoError, ...{emailError: true}})) : 
            setInfoError(infoError => ({...infoError, ...{emailError: false}}));
        loginReq.username === "" ? 
            setInfoError(infoError => ({...infoError, ...{usernameError: true}})) : 
            setInfoError(infoError => ({...infoError, ...{usernameError: false}}));
        loginReq.password === "" ?
            setInfoError(infoError => ({...infoError, ...{passwordError: true}})) : 
            setInfoError(infoError => ({...infoError, ...{passwordError: false}}));
        
        if (loginReq.email !== "" && loginReq.username !== "" && loginReq.password !== "")
        {
            const registered = await RegisterAPI(loginReq);
            if (registered)
            {
                // Return to home page
                navigate("/")
            }
        }
    }

    return (
        <>
            <div className="login-layout">
                <h1>💪 Workout Tracker</h1>
                <h4>Register new account</h4>
                <div className="login-container">
                    <form method="post" onSubmit={ValidateRegister}>
                        <div className="login-input-container">
                            <div className="login-input">
                                <label>Email: </label>
                                <input type="email" name="email" placeholder='email'></input>
                            </div>
                            {infoError.emailError && <p style={{ color: 'red' }}><b>Email cannot be empty</b></p>}
                        </div>
                        <div className="login-input-container">
                            <div className="login-input">
                                <label>Username: </label>
                                <input type="text" name="username" placeholder='username'></input>
                            </div>
                            {infoError.usernameError && <p style={{ color: 'red' }}><b>Username cannot be empty</b></p>}
                        </div>
                        <div className="login-input-container">
                            <div className="login-input">
                                <label>Password: </label>
                                <input type="password" name="password" placeholder='password'></input>
                            </div>
                            {infoError.passwordError && <p style={{ color: 'red' }}><b>Password cannot be empty</b></p>}
                            {registerError && !infoError.emailError && !infoError.usernameError && !infoError.passwordError && <p style={{ color: 'red' }}><b>Email/username is already taken - try again</b></p>}
                        </div>
                        <div className="login-input-container">
                            <button typ="submit">Register</button>
                        </div>
                    </form>
                </div>
                <div className="login-register">
                    <Link to="/login">Already have an account? Log in</Link>
                </div>
            </div>
        </>
    );

    async function RegisterAPI(reqData) {
        const resp = await fetch("http://localhost:5286/User/Register", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(reqData)
        });

        if (resp.status === 201) {
            setRegisterError(false);
            const respJson = await resp.json();
            console.log(respJson);
            return true;
        }

        setRegisterError(true);
        return false;
    }
}

export default Register