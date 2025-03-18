import { logOutEndpoint, signOutOptions } from "../api/options";
import { useNavigate } from "react-router";
import { useAppSelector } from "../hooks/hooks";
function Dashboard() {

    const navigate = useNavigate();

    const userState = useAppSelector((state) => state.UserName);

    async function LogOut(event: React.MouseEvent<HTMLButtonElement>) {
        event.preventDefault();

        const options = signOutOptions();

        fetch(`${import.meta.env.VITE_API_BASE_URL}` + `${logOutEndpoint}`, options)
            .then(res => console.log(res))
            .then(data => {
                navigate("/");
            }).catch(err => {
                throw new Error("" + err);
            })

    }
  return (
      <div>
          <h1>Welcome, {userState}!</h1>
          <button type="submit" onClick={async (event) => {
              await LogOut(event);
          }}>Log Out</button>
      </div>
  );
}

export default Dashboard;