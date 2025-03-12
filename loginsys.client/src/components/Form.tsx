import "../styles/formStyle.css";

function Form({ setEmail, setPass, submitForm }: React.ComponentState) {

    function handleInputChange(event: React.ChangeEvent<HTMLInputElement>) {
        const { name, value } = event.target;

        if (name === "Email") setEmail(value);
        if (name === "Pass") setPass(value);
    }

    return (
        <div className="authContainer">
            <form>
                <div className="formContainer">
                    <label htmlFor="email">Enter your email:</label>
                    <input className="userInput" name="Email" id="email" type="text" onChange={event => {
                        handleInputChange(event);
                    }}></input>
                    <label htmlFor="password">Enter your password:</label>
                    <input className="userInput" name="Pass" id="password" type="password" onChange={event => {
                        handleInputChange(event);
                    }}></input>

                    <button type="submit" onClick={async (event) => {
                        await submitForm(event);
                    }}>Submit</button>
                </div>
            </form>
        </div>
    );
}

export default Form;