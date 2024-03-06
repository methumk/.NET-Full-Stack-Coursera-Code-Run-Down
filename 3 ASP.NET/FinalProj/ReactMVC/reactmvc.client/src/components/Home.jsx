import { Link } from 'react-router-dom';
import { cookieStrings, CookieManager } from '../models/cookieModel.js';
import Navbar from './Navbar.jsx';
import { TodayWorkoutSession } from './Workouts.jsx';
import '../componentcss/Home.css'

function WorkoutLoggedOut()
{
    return (
        <>
        <div className='main-display'>
            <h1 className='main-display-header'>Log in to View Workouts</h1>
        </div>
        </>
    );
}

function TodaysWorkout({token})
{
    return (
        <>
        <div className='main-display'>
            <h1 className='main-display-header'>Workouts Logged Today</h1>
            <TodayWorkoutSession token={token} />
        </div>
        </>
    );
}

function Home()
{
    const loginObj = new CookieManager(cookieStrings.loginObj).toJson();
    console.log(loginObj);

    return (
        <>
        <Navbar userLoginObj={loginObj}/>
        {loginObj !== null ? <TodaysWorkout token={loginObj.token}/> : <WorkoutLoggedOut />}
        </>
    );
}

export default Home