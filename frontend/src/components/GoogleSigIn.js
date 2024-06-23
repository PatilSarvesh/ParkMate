import { GoogleOAuthProvider, GoogleLogin } from "@react-oauth/google";
import { jwtDecode } from "jwt-decode";
import axios from 'axios';
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { useUser } from "../UserContext/UserContext";


const GoogleSignIn = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const navigate = useNavigate();
  const {setUser} = useUser();

  const handleSuccess = async (response) => {
    // Handle login success and redirect to home
    var userObj = jwtDecode(response.credential);

    const User = {
      name: userObj.name,
      email: userObj.email,
      picture: userObj.picture,
      token: response.credential,
    };

    const data = await axios.post(`${process.env.REACT_APP_BASE_API_URL}/CreateUser`, User);

    if(data.status === 200){
      setIsAuthenticated(true)
      setUser(data.data);
      localStorage.setItem('token', data.data.jwtToken);
      navigate("/Home");
    }
    // if(isAuthenticated)
    //   {

    //   }
    // console.log(data);
  };

  const handleError = () => {
    console.log("Login Failed");
  };

  return (
    <GoogleOAuthProvider clientId={process.env.REACT_APP_GOOGLE_CLIENT_ID}>
      <div className="flex justify-center items-center h-screen">
        <GoogleLogin
          onSuccess={handleSuccess}
          onError={handleError}
          cookiePolicy={"single_host_origin"}
        />
      </div>
    </GoogleOAuthProvider>
  );
};

export default GoogleSignIn;
