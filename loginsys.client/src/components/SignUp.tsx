import { useState } from "react";


interface User {

    UserName: string,
    UserPassword: string
}
function SignUp() {

    const [userName, setUserName] = useState("");

    const [pass, setPass] = useState("");


    function handleUserName(event: React.ChangeEvent<HTMLInputElement>) {
        setUserName(event.target.value);
    }

    function handlePassword(event: React.ChangeEvent<HTMLInputElement>) {
        setPass(event.target.value);
    }

    function submitForm(event: React.MouseEvent<HTMLButtonElement>) {
        event.preventDefault();
        const user: User = { 
            UserName: userName,
            UserPassword: pass
        }

        fetch("https://localhost:7268/signup", {
            headers: {
                "Content-Type": "application/json",
                "Access-Control-Allow-Origin": "https://localhost:5173/"
            },
            method: "POST",
            body: JSON.stringify(user)
        }).then(res => {
            console.log(res);
        }).catch(err => {
            console.log(err);
            throw new Error(err.message);
        });
    }

  return (
      <div>
          <form>
              <input className="userInput" name="Username" type="text" onChange={event => {
                  handleUserName(event);
              }}></input>
              <input className="userInput" name="Pass" type="password" onChange={event => {
                  handlePassword(event);
              }}></input>

              <button type="submit" onClick={event => {
                  submitForm(event);
              }}>Submit</button>
          </form>
      </div> 
  );
}

export default SignUp;