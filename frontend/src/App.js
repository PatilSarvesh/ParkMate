import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import GoogleSignIn from './components/GoogleSigIn';
import Home from './pages/Home';
import Park from './pages/Park';
import Exit from './pages/Exit';
import SlotBooking from './pages/SlotBooking';
import Layout from './components/Layout';
import { UserProvider } from './UserContext/UserContext';


function App() {
  return (
    <UserProvider>
      <Router>
        <Routes>
          <Route path="/" element={<GoogleSignIn />} />
          <Route element={<Layout />}>
            <Route path="/home" element={<Home />} />
            <Route path="/park" element={<Park />} />
            <Route path="/exit" element={<Exit />} />
            <Route path="/slotBooking" element={<SlotBooking />} />
          </Route>
        </Routes>
      </Router>
    </UserProvider>
  );
}

export default App;
