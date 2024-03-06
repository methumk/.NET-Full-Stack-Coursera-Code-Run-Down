import { Link, useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';
import { cookieStrings, CookieManager } from '../models/cookieModel.js';
import Navbar from './Navbar.jsx';
import '../componentcss/Workouts.css'

const MAX_WORKOUTS_PER_PAGE = 2;

function getCurrentDateAsString() 
{
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    const month = String(currentDate.getMonth() + 1).padStart(2, '0'); // Months are zero-indexed
    const day = String(currentDate.getDate()).padStart(2, '0');
  
    return `${year}-${month}-${day}`;
}

function DisplayWorkoutBySessions({workoutsBySessions, setWorkoutModified=null})
{
    const [editRow, setEditRow] = useState({});
    const loginObj = new CookieManager(cookieStrings.loginObj).toJson();

    const handleEditRow = (workoutId, fieldName, changeValue) => {

        if (editRow?.workoutId === workoutId)
        {
            setEditRow({
                ...editRow,
                [fieldName] : changeValue
            });
        }else{
            setEditRow({
                workoutId: workoutId,
                [fieldName] : changeValue
            });
        }
    }

    const handleEdit = (workout) => {

        if (editRow?.workoutId === workout.id)
        {
            var {id, userId, user, ...workoutData} = workout;
            for (const key in editRow)
            {
                if (key in workoutData)
                {
                    workoutData[key] = editRow[key];
                    if (key !== 'exercise')
                    {
                        workoutData[key] = parseFloat(workoutData[key])
                    }
                }
            }

            EditWorkout(loginObj.token, id, workoutData);
        }
        setEditRow({});
        
    }

    const handleDelete = (workoutId) => {
        deleteWorkout(loginObj.token, workoutId);
    }

    return (
        <>
        {
            workoutsBySessions.map(workoutSessions => {
                return (
                    <>
                    <div className='workout-data-date'>
                        <h3>Workout for {workoutSessions.date}</h3>
                    </div>
                    <div className='workout-data-table-container'>
                        <table className='workout-data-table'>
                            <thead>
                                <tr key={workoutSessions.date}>
                                    <th>Exercise</th>
                                    <th>Weight (lb)</th>
                                    <th>Sets</th>
                                    <th>Reps</th>
                                    <th>Rest (min)</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                {workoutSessions.workoutInfoByDate.map(workout => {
                                    return (
                                        <tr key={workout.id}>
                                            <td>
                                                <input className='workout-row-data' type="text" name="exercise" required 
                                                    onChange={(e) => {handleEditRow(workout.id, "exercise", e.target.value)}}
                                                    placeholder={workout.exercise}/>
                                            </td>
                                            <td>
                                                <input className='workout-row-data' type="number" name="weight" required 
                                                    onChange={(e) => {handleEditRow(workout.id, "weight", e.target.value)}}
                                                    placeholder={workout.weight}/>
                                            </td>
                                            <td>
                                                <input className='workout-row-data' type="number" name="sets" required 
                                                    onChange={(e) => {handleEditRow(workout.id, "sets", e.target.value)}}
                                                    placeholder={workout.sets}/>
                                            </td>
                                            <td>
                                                <input className='workout-row-data' type="number" name="reps" required 
                                                    onChange={(e) => {handleEditRow(workout.id, "reps", e.target.value)}}
                                                    placeholder={workout.reps}/>
                                            </td>
                                            <td>
                                                <input className='workout-row-data' type="number" step="0.1" 
                                                    onChange={(e) => {handleEditRow(workout.id, "rest", e.target.value, workout)}}
                                                    placeholder={workout.rest ? workout.rest : "-"}/>
                                            </td>
                                            <td className='workout-row-btn'>
                                                <button className='table-edit-btn' onClick={() => {handleEdit(workout)}}>Edit</button>
                                            </td>
                                            <td className='workout-row-btn'>
                                                <button className='table-del-btn' onClick={() => {handleDelete(workout.id)}}>Delete</button>
                                            </td>
                                        </tr>
                                    )
                                })}
                            </tbody>
                        </table>
                    </div>  
                    
                </>
                )
            })
        }
    </>
    );

    function EditWorkout(token, workoutId, workoutData)
    {
        fetch(`http://localhost:5286/Workout/${workoutId}`, {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json',
                'Authorization' : `Bearer ${token}`
            },
            body: JSON.stringify(workoutData)
        })
        .then(resp => {
            if (resp.ok)
            {
                if (setWorkoutModified !== null)
                {
                    setWorkoutModified(w => w + 1);
                }
            }
        })
        .catch(error => {
            console.log(`Error editing id: ${workoutId}`);
        });
    }

    function deleteWorkout(token, workoutId)
    {
        fetch(`http://localhost:5286/Workout/${workoutId}`, {
            method: 'DELETE',
            headers: {
                'Authorization' : `Bearer ${token}`
            }
        })
        .then(resp => {
            if (resp.ok)
            {
                console.log(`Deleted workout: ${workoutId}`);
                if (setWorkoutModified !== null)
                {
                    setWorkoutModified(w => w + 1);
                }
            }
        })
        .catch(error => {
            console.log(`Error deleting id: ${workoutId}`);
        });
    }
}

export function TodayWorkoutSession({token})
{
    const [workouts, setWorkouts] = useState(null);
    const [workoutModified, setWorkoutModified] = useState(0);

    useEffect(() => {
        GetLatestWorkout(token);
    }, [workouts, workoutModified])
    
    return (
        <>
        <div className='workout-data-container'>
            {
                workouts === null ? 
                <h5>No workout data for today</h5> :
                <DisplayWorkoutBySessions workoutsBySessions={workouts} setWorkoutModified={setWorkoutModified}/>
            }
        </div>
        </>
    );

    function GetLatestWorkout(token)
    {
        fetch(`http://localhost:5286/Workout/Dates?page=1&recs=1`, {
            method: 'GET',
            headers: {
                'Authorization' : `Bearer ${token}`
            }
        })
        .then(resp => resp.json())
        .then(json => {
            var today = getCurrentDateAsString();
            if (json.length > 0 && today === json[0].date)
            {
                setWorkouts(json);
            }else{
                setWorkouts(null);
            }
        })
        .catch(error => {
            console.log(`Error: ${error} when getting home workouts`);
            setWorkouts(null);
        })
    }
}

export function WorkoutSession({ currPage, token, maxCount=MAX_WORKOUTS_PER_PAGE })
{
    const [workouts, setWorkouts] = useState(null);
    const [workoutModified, setWorkoutModified] = useState(0);

    useEffect(() => {
        GetPageWorkouts(token, currPage, maxCount);
    }, [currPage, workoutModified])

    return (
        <>
        <div className='workout-data-container'>
            {
                workouts === null ? 
                <h5>You have no logged workouts</h5> :
                <DisplayWorkoutBySessions workoutsBySessions={workouts} setWorkoutModified={setWorkoutModified}/>
            }
        </div>
        </>
    );

    function GetPageWorkouts(token, page, maxCount=MAX_WORKOUTS_PER_PAGE)
    {
        fetch(`http://localhost:5286/Workout/Dates?page=${page}&recs=${maxCount}`, {
            method: 'GET',
            headers: {
                'Authorization' : `Bearer ${token}`
            }
        })
        .then(resp => resp.json())
        .then(json => {
            if (json.length > 0)
            {
                setWorkouts(json);
            }else{
                setWorkouts(null);
            }
            
        })
        .catch(error => {
            console.log(`Error: ${error} when getting workout for page: ${page}`);
            setWorkouts(null);
        })
    }
}

function AllWorkouts()
{
    const [currPage, setCurrPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const navigate = useNavigate();
    const cookieManJson = new CookieManager(cookieStrings.loginObj).toJson();

    useEffect(() => {
        if (cookieManJson === null)
        {
            navigate('/')
        }else{
            GetTotalWorkoutCount(cookieManJson.token);
        }
    }, [currPage, cookieManJson]);

    const handleNextWorkout = () => {
        if (currPage > 1)
        {
            setCurrPage(currPage-1)
        }
    }

    const handlePreviousWorkout = () => {
        if (currPage < totalPages)
        {
            setCurrPage(currPage+1)
        }
    }

    return (
        <>
        <Navbar 
            userLoginObj={cookieManJson}
            enableAllWorkouts={false}
            enableProfile = {false}
        />
        {
            cookieManJson !== null && 
            <div className='workout-display-container'>
                <div className='workout-display-header'>
                    <h1>{cookieManJson.username}'s Workouts</h1>
                    <h4>All logged workouts</h4>
                </div>
                <div className='workout-data-panel'>
                    <Link to="/create"><button>🏋️‍♀️ Create Workout</button></Link>
                    {(currPage > 1) && 
                        <button onClick={handleNextWorkout}>
                            ⏪Next Workouts ({currPage-1}/{totalPages})
                        </button>}
                    {(currPage < totalPages) && 
                        <button onClick={handlePreviousWorkout}>
                            ⏩Previous Workouts ({currPage+1}/{totalPages})
                        </button>}
                </div>
                <WorkoutSession 
                    currPage={currPage} 
                    token={cookieManJson.token}/>
            </div>
        }
        
        </>
    );

    function GetTotalWorkoutCount(token)
    {
        fetch("http://localhost:5286/Workout/Count", {
            method: 'GET',
            headers: {
                'Authorization' : `Bearer ${token}`
            }
        })
        .then(resp => resp.json())
        .then(json => {
            const totalPages = Math.round(json.count/MAX_WORKOUTS_PER_PAGE);
            console.log(`Total workouts: ${json.count} - total pages: ${totalPages}`);
            setTotalPages(totalPages);
        })
        .catch(e =>{
            console.log(`Caught error: ${e}`);
            setTotalPages(1);
        })
    }
}
export default AllWorkouts