import React, { useState } from 'react';
import axios from 'axios';
import Modal from 'react-modal';
import { address_url } from '../Service/url';
import { useUser } from './UserContext';
import { Link, useNavigate } from 'react-router-dom';
import Avatar from 'react-avatar';

Modal.setAppElement('#root');

const customStyles = {
  content: {
    top: '50%',
    left: '50%',
    right: 'auto',
    bottom: 'auto',
    marginRight: '-50%',
    transform: 'translate(-50%, -50%)',
    zIndex: 4
  },
};

const Login = () => {
  const navigate = useNavigate();
  const { user, setUser } = useUser();
  const [visible, setVisible] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const userNameToUpperCase = user.username.charAt(0).toUpperCase() + user.username.slice(1);

  async function loginUser() {
    try {
      const response = await axios.post(`${address_url}/Auth/Login`, {
        Email: email,
        Password: password
      });
      if (response.status >= 200 && response.status < 300) {
        console.log('User Logged successfully!');
        console.log(response)

        const tokenParts = response.data.Token.split('.');
        const decodedPayload = JSON.parse(atob(tokenParts[1]));
        console.log(decodedPayload.Id)

        setUser({
          id: decodedPayload.Id,
          username: response.data.UserName,
          email: response.data.Email,
          refreshtoken: response.data.RefreshToken,
          token: response.data.Token,
          isLogged: true
        });
        setVisible(false);
      }
    }
    catch (error) {
      console.error(error);
    }
  }

  function logout() {
    setUser({
      id: '',
      username: '',
      email: '',
      refreshtoken: '',
      token: '',
      isLogged: false
    });
    setEmail('');
    setPassword('');
    navigate('/');
  }

  return (

    <div className='signInButton'>
      {!user.isLogged ? (
        <>
          <button className="buttonSignIn" onClick={() => { setVisible(true); }}>Sign In</button>
          <Modal isOpen={visible} onRequestClose={() => setVisible(false)} style={customStyles}>
            <div className="modal-content">
              <input
                className="input-black"
                type="text"
                placeholder="Email"
                value={email}
                onChange={e => setEmail(e.target.value)}
              />
              <input
                className="input-black"
                type="password"
                placeholder="Password"
                value={password}
                onChange={e => setPassword(e.target.value)}
              />
              <button className="buttonLogin" onClick={loginUser}>Login</button>
              <button className="buttonLogin" onClick={() => setVisible(false)}>Back</button>
            </div>
          </Modal>
        </>
      ) : (
        <div className='welcome-section-container'>
          <div className="welcome-section">
            <div className="icon-container"><Link to="/profile" className="usernameProfiles">
              <Avatar className="user-avatar" size="50" round={true} name={user.username} />
              <p>{userNameToUpperCase}</p></Link>
            </div>

            <div className="icon-container" onClick={logout}>
              <span className="material-symbols-outlined" onClick={logout} >
                logout
              </span>
              <p className="logoutText" >Logout</p>
            </div>
          </div>
        </div>
      )}
    </div>

  )
}

export default Login;