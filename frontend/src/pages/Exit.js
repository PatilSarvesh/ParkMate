import { useState } from 'react';

const Exit = () => {
  const [bookingNumber, setBookingNumber] = useState('');

  const handleExit = () => {
    // Implement exit logic
    console.log('Exiting with Booking Number:', bookingNumber);
  };

  return (
    <div className="flex flex-col items-center justify-center h-screen bg-gray-100">
      <h1 className="text-2xl font-bold mb-6">Exit Parking</h1>
      <input
        type="text"
        placeholder="Enter Booking Number"
        value={bookingNumber}
        onChange={(e) => setBookingNumber(e.target.value)}
        className="mb-4 px-4 py-2 border rounded-lg shadow-md"
      />
      <button
        className="bg-red-500 text-white px-6 py-2 rounded-lg shadow-md hover:bg-red-600"
        onClick={handleExit}
        disabled={!bookingNumber}
      >
        Exit
      </button>
    </div>
  );
};

export default Exit;
