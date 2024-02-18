import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import './index.css'

// Create H1 element in JSX
// const greeting = <h1>Hello, World in JSX</h1>

// More complex with react elements
const greeting = React.createElement('h1', null, 'Hello, World with React Element')

// New react component that returns a root object that allows async rendering
// we call render object on root object and pass app component as an argument to render it into the HTML DOM element with ID of root
// The root id is present in the index.html and is a div inside the body
ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    {greeting}
    <App />
  </React.StrictMode>,
)
