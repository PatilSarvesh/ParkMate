import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { FaMotorcycle, FaCar, FaBicycle } from 'react-icons/fa';
import axios from 'axios';

const Park = () => {
  const [vehicleType, setVehicleType] = useState('');
  const [selectedSlot, setSelectedSlot] = useState(null);
  const [bikeSlots, setBikeSlots] = useState([]);
  const [carSlots, setCarSlots] = useState([]);
  const [bicycleSlots, setBicycleSlots] = useState([]);
  const navigate = useNavigate();

  const handleBooking = () => {
    navigate(`/slotBooking?slotId=${selectedSlot}&type=${vehicleType}`);
  };
  // const LoadJobPage = async () => {
  //   navigate(`/Job?id=${job.jobId}`);
  // };
  useEffect(() => {
    const fetchAllSlots = async () => {
      try {
        const response = await axios.get(`${process.env.REACT_APP_BASE_API_URL}/GetAllSlots`);
        const slots = response.data;

        // Categorize the slots by type
        setBikeSlots(slots.filter(slot => slot.type === 'Bike'));
        setCarSlots(slots.filter(slot => slot.type === 'Car'));
        setBicycleSlots(slots.filter(slot => slot.type === 'Bicycle'));
      } catch (error) {
        console.error('Error fetching slots:', error);
      }
    };

    fetchAllSlots();
  }, []);

  // Determine the slots to display based on selected vehicle type
  const slotsToDisplay = vehicleType === 'bike' ? bikeSlots
                      : vehicleType === 'car' ? carSlots
                      : vehicleType === 'bicycle' ? bicycleSlots
                      : [];

  return (
    <div className="flex flex-col items-center justify-center h-screen bg-gray-100 dark:bg-gray-900">
      <h1 className="text-3xl font-bold mb-6 text-gray-900 dark:text-gray-200">Select a Vehicle Type</h1>
      <div className="flex mb-6">
        <button
          className={`p-4 mx-2 flex items-center rounded-lg shadow-md ${vehicleType === 'bike' ? 'bg-green-500 text-white' : 'bg-white text-black'}`}
          onClick={() => setVehicleType('bike')}
        >
          <FaMotorcycle className="mr-2" />
          Bike
        </button>
        <button
          className={`p-4 mx-2 flex items-center rounded-lg shadow-md ${vehicleType === 'car' ? 'bg-green-500 text-white' : 'bg-white text-black'}`}
          onClick={() => setVehicleType('car')}
        >
          <FaCar className="mr-2" />
          Car
        </button>
        <button
          className={`p-4 mx-2 flex items-center rounded-lg shadow-md ${vehicleType === 'bicycle' ? 'bg-green-500 text-white' : 'bg-white text-black'}`}
          onClick={() => setVehicleType('bicycle')}
        >
          <FaBicycle className="mr-2" />
          Bicycle
        </button>
      </div>
      <h1 className="text-3xl font-bold mb-6 text-gray-900 dark:text-gray-200">Select a Parking Slot</h1>
      <div className="grid grid-cols-4 gap-4 mb-6">
        {slotsToDisplay.map(slot => (
          <button
            key={slot.slotId}
            className={`p-4 rounded-lg shadow-md ${selectedSlot === slot.slotId ? 'bg-green-500 text-white' : 'bg-white text-black'}`}
            onClick={() => setSelectedSlot(slot.slotId)}
            disabled={!slot.isAvailable}
          >
            Slot {slot.slotId}
          </button>
        ))}
      </div>
      <button
        className="bg-blue-500 text-white px-8 py-2 rounded-lg shadow-md hover:bg-blue-600 transition duration-200"
        onClick={handleBooking}
        disabled={!selectedSlot}
      >
        Book Slot
      </button>
    </div>
  );
};

export default Park;
