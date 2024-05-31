import React from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from './Navbar';  // Adjust the import path as needed

const Layout = ({ username }) => {
  return (
    <div>
      <Navbar username={username} />
      <Outlet />
    </div>
  );
};

export default Layout;
