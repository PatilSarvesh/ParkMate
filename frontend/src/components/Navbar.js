import React, { useState, useEffect } from 'react';
import { FaUserCircle, FaSun, FaMoon } from 'react-icons/fa';
import { useUser } from '../UserContext/UserContext';

const Navbar = ({ username }) => {
  const [darkMode, setDarkMode] = useState(false);
  const {user} = useUser();

  useEffect(() => {
    const savedMode = localStorage.getItem('darkMode') === 'true';
    setDarkMode(savedMode);
    document.documentElement.classList.toggle('dark', savedMode);
  }, []);

  const toggleDarkMode = () => {
    setDarkMode(!darkMode);
    localStorage.setItem('darkMode', !darkMode);
    document.documentElement.classList.toggle('dark', !darkMode);
  };

  return (
    <nav className="bg-blue-500 p-4 shadow-md dark:bg-gray-800">
      <div className="container mx-auto flex justify-between items-center">
        <div className="text-white text-2xl font-bold dark:text-gray-200">
          Parking App
        </div>
        <div className="flex items-center">
          <button
            onClick={toggleDarkMode}
            className="mr-4 focus:outline-none"
          >
            {darkMode ? <FaSun className="text-yellow-500" /> : <FaMoon className="text-gray-300" />}
          </button>
          {user ? (
            <img
              src={user.picture}
              alt={user.name}
              className="h-8 w-8 rounded-full mr-2"
            />
          ) : (
            <FaUserCircle className="text-white text-3xl mr-2  dark:text-gray-200" />
          )}
          {/* <FaUserCircle className="text-white text-3xl mr-2" /> */}
          <span className="text-white text-lg dark:text-gray-200">{user.name}</span>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
