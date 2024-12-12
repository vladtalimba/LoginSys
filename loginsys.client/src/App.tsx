import './App.css';
import SignUp from "./pages/SignUp";
import Login from "./pages/LogIn";
import { BrowserRouter, Routes, Route } from "react-router-dom";

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="signup" element={<SignUp />} />
            </Routes>
        </BrowserRouter>
    )
}

export default App;