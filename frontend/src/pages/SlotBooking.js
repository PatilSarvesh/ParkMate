import axios from "axios";
import React, { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useUser } from "../UserContext/UserContext";

const BookingConfirmation = () => {
  const { user } = useUser();
  console.log(user);
  const location = useLocation();
  const navigate = useNavigate();
  const today = new Date().toISOString().slice(0, 16); 

  const params = new URLSearchParams(location.search);
  const selectedSlot = params.get("slotId");
  const vehicleType = params.get("type");

  const [formData, setFormData] = useState({
    VechileNumber: "",
    ownerName: "",
    parkingEntry: "",
    contactNumber: "",
  });

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const [timeLeft, setTimeLeft] = useState(60); // Initial time in seconds

  useEffect(() => {
    // Countdown timer
    const countdownInterval = setInterval(() => {
      setTimeLeft(prevTime => prevTime - 1);
    }, 1000);

    // Timeout to redirect after 1 minute
    const timer = setTimeout(() => {
      handleCancel();
     // navigate("/Park");
    }, 60000);

    // Cleanup intervals and timeout if the component unmounts or if the user confirms the booking
    return () => {
      clearInterval(countdownInterval);
      clearTimeout(timer);
    };
  }, [navigate]);



  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(location);

    const slot = {
      VechileNumber: formData.VechileNumber,
      parkingEntry: formData.parkingEntry,
      slotId: selectedSlot,
      VechileType: vehicleType,
      UserId: user.userId,
    };

    try {
      const response = await axios.post(
        `${process.env.REACT_APP_BASE_API_URL}/BookSlot`,
        slot
      );
     // await axios.post(`${process.env.REACT_APP_BASE_API_URL}/UnlockSpot/${selectedSlot}?isBookingConfirmed=true`);
      console.log("Form Data Submitted: ", response.data);
      navigate("/home");
    } catch (error) {
      console.error("Error submitting form data:", error);
    }
  };

  const handleCancel = async () => {
    // Add any additional logic if needed
    await axios.post(`${process.env.REACT_APP_BASE_API_URL}/UnlockSpot/${selectedSlot}?isBookingConfirmed=false`);
    navigate('/Park'); // Redirect to the home page ("/")
  };

  return (
    
    <div className="flex flex-col items-center justify-center h-screen bg-gray-100 dark:bg-gray-900">

      <h1 className="text-4xl font-bold mb-8 text-gray-900 dark:text-gray-200">
        Booking Confirmation
      </h1>
      
      <div className="bg-white p-8 rounded-lg shadow-md dark:bg-gray-800">
        <p className="text-xl text-gray-900 dark:text-gray-200 mb-4">
          Thank you for booking!
        </p>
        <p className="text-lg text-gray-700 dark:text-gray-400 mb-2">
          Vehicle Type: {vehicleType}
        </p>
        <p className="text-lg text-gray-700 dark:text-gray-400 mb-2">
          Slot Number: {selectedSlot}
        </p>
        <p className="text-lg text-red-600 mb-4">
          You have {timeLeft} seconds to complete the booking.
        </p>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="block text-gray-700 dark:text-gray-400">
              Vehicle Number
            </label>
            <input
              type="text"
              name="VechileNumber"
              value={formData.VechileNumber}
              onChange={handleChange}
              className="w-full p-2 rounded-md border dark:bg-gray-700 dark:border-gray-600 dark:text-gray-200"
              required
            />
          </div>
          <div>
            <label className="block text-gray-700 dark:text-gray-400">
              Entry Time
            </label>
            <input
              type="datetime-local"
              name="parkingEntry"
              value={formData.parkingEntry}
              min={today}
              onChange={handleChange}
              className="w-full p-2 rounded-md border dark:bg-gray-700 dark:border-gray-600 dark:text-gray-200"
              required
            />
          </div>
          <div>
            <label className="block text-gray-700 dark:text-gray-400">
              Contact Number
            </label>
            <input
              type="text"
              name="contactNumber"
              value={formData.contactNumber}
              onChange={handleChange}
              className="w-full p-2 rounded-md border dark:bg-gray-700 dark:border-gray-600 dark:text-gray-200"
              required
            />
          </div>
          <button
            type="submit"
            className="bg-blue-500 text-white px-6 py-2 rounded-lg shadow-md hover:bg-blue-600 transition duration-200"
          >
            Confirm Booking
          </button>
          <button
            type="button"
            className="bg-blue-500 text-white px-6 py-2 rounded-lg shadow-md hover:bg-blue-600 transition duration-200"
            onClick={handleCancel}
          >
            Cancel Booking
          </button>
        </form>
      </div>
    </div>
  );
};

export default BookingConfirmation;
