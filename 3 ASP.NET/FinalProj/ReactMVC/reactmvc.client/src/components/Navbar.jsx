import { Link } from 'react-router-dom';
import { cookieStrings, CookieManager } from '../models/cookieModel.js';
import '../componentcss/Home.css'

function Navbar({userLoginObj, enableAllWorkouts=true, enableProfile = true})
{
    console.log(`Login obj: ${userLoginObj}`)
    const registerData = userLoginObj !== null ? `${userLoginObj.username}'s Profile` : 'Register';
    const registerDataLink = userLoginObj !== null ? '/' : '/register';
    const loginData = userLoginObj !== null ? 'Logout' : 'Login';
    const loginDataLink = userLoginObj !== null ? '/logout' : '/login';

    return (
        <>
        <div className='app-navbar'>
            <div className='left-navbar-items'>
                <Link to="/" className='navbar-items'>MyWorkoutTracker</Link>
                {enableAllWorkouts && <Link to="/workouts" className='navbar-items'>All Workouts</Link>}
            </div>
            <div className='right-navbar-items'>
                {enableProfile && <Link to={registerDataLink} className='navbar-items'>{registerData}</Link>}
                <Link to={loginDataLink} className='navbar-items'>{loginData}</Link>
            </div>
        </div>
        </>
    );
}
export default Navbar