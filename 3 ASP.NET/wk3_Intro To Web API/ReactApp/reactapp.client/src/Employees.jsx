import { useEffect, useState } from 'react';
import './App.css'

function FetchEmployees()
{
    const [employees, setEmployees] = useState();

    useEffect(() => {
        GetEmployees();
    }, []);
    

    const content = employees ===  undefined 
    ? <p><em>Data loading...</em></p> 
    : <table>
        <thead>
            <tr>
                <td>Name</td>
                <td>City</td>
                <td>Department</td>
                <td>Gender</td>
            </tr>
        </thead>
        <tbody>
            {employees.map(emp =>
                <tr key={emp.employeeId}>
                    <td>{emp.name}</td>
                    <td>{emp.city}</td>
                    <td>{emp.department}</td>
                    <td>{emp.gender}</td>
                </tr>
            )}
        </tbody>
    </table>

    return (
        <div>
            <h1 id="Employees-Header">All Employees</h1>
            {content}
        </div>
    );

    async function GetEmployees()
    {
        const resp = await fetch('http://localhost:5126/Employees');
        if (resp.status === 200)
        {
            console.log(`Resp status 200`)
        }else {
            console.log(`Resp status: ${resp}`)
        }
        const data = await resp.json();
        console.log(`Data: ${data}`)
        setEmployees(data);
    }
}



export default FetchEmployees;