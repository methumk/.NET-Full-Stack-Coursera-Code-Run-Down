import { Link, useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';
import { cookieStrings, CookieManager } from '../models/cookieModel.js';
import Navbar from './Navbar.jsx';

function Create()
{
    const navigate = useNavigate();
    const loginObj = new CookieManager(cookieStrings.loginObj).toJson();


    useEffect(() => {
        if (loginObj === null)
        {
            navigate('/')
        }
    }, []);


    const handleCreateWorkout = (e) => {
        // Prevent the browser from reloading the page
        e.preventDefault();

        const form = e.target;
        const formData = new FormData(form);
        
        const newWorkout = {
            exercise: formData.get("exercise"),
            weight: parseFloat(formData.get("weight")),
            sets: parseFloat(formData.get("sets")),
            reps: parseFloat(formData.get("reps")),
            date: formData.get("date"),
        };

        if (formData.get("Rest") !== "")
        {
            newWorkout.rest = parseFloat(formData.get("rest"));
        }

        CreateWorkout(loginObj.token, newWorkout);
        navigate('/workouts');
    }

    return (
        <>
            <Navbar 
                userLoginObj={loginObj}
            />
            <div className='edit-container'>
                <div className='edit-title'>
                    <h2>Create workout</h2>
                </div>
                <form method="POST" onSubmit={handleCreateWorkout} className='edit-fields'>
                    <div className='workout-fields'>
                        <label>Exercise: </label>
                        <input type="text" name="exercise" placeholder='exercise' required></input>
                    </div>
                    <div className='workout-fields'>
                        <label>Weight (lb): </label>
                        <input type="number" name="weight" min={0} placeholder='weight' required></input>
                    </div>
                    <div className='workout-fields'>
                        <label>Sets: </label>
                        <input type="number" name="sets" min={0} placeholder='sets' required></input>
                    </div>
                    <div className='workout-fields'>
                        <label>Reps: </label>
                        <input type="number" name="reps" min={0} placeholder='reps' required></input>
                    </div>
                    <div className='workout-fields'>
                        <label>Rest (min): </label>
                        <input type="number" step="0.01" min={0} name="rest" placeholder='rest'></input>
                    </div>
                    <div className='workout-fields'>
                        <label>Date: </label>
                        <input type="date" name="date" placeholder='date' required></input>
                    </div>
                    <div>
                        <button type="submit">Save</button>
                        <Link to="/workouts"><button>Cancel</button></Link>
                    </div>
                </form>
            </div>
        </>
    );

    function CreateWorkout(token, newWorkout)
    {
        fetch("http://localhost:5286/Workout", {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization' : `Bearer ${token}`
            },
            body: JSON.stringify(newWorkout)
        })
        .then(resp => {
            if (resp.ok)
            {
                console.log(`Created workout`);
            }
        })
        .catch(error => {
            console.log(`Error when creating new workout: ${error}`);
        })

    }
}
export default Create