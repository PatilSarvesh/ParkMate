import { useState } from 'react';
import axios from 'axios';


const Exit = () => {
  const [bookingNumber, setBookingNumber] = useState('');
  const [bookingDetails, setBookingDetails] = useState(null);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [paymentProcessing, setPaymentProcessing] = useState(false);

  const fetchBookingDetails = async () => {
    try {
      setLoading(true);
      const response = await axios.get(`${process.env.REACT_APP_BASE_API_URL}/GetBookingDetails?bookingNumber=${bookingNumber}`);
      setBookingDetails(response.data);
      console.log(response.data);
      setError('');
    } catch (err) {
      setError(err.response?.data?.message || 'Error fetching booking details');
      setBookingDetails(null);
    } finally {
      setLoading(false);
    }
  };

  const handlePayment = async () => {
    try {
      setPaymentProcessing(true);
      const response = await axios.post('/api/parking/pay', { bookingId: bookingNumber });
      alert(response.data.message);
      setBookingDetails(null);
      setBookingNumber('');
    } catch (err) {
      setError(err.response?.data?.message || 'Error processing payment');
    } finally {
      setPaymentProcessing(false);
    }
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
        className="bg-blue-500 text-white px-6 py-2 rounded-lg shadow-md hover:bg-blue-600"
        onClick={fetchBookingDetails}
        disabled={!bookingNumber || loading}
      >
        Fetch Booking Details
      </button>

      {error && <p className="text-red-500 mt-4">{error}</p>}

      {bookingDetails && (
        <div className="mt-6 p-4 border rounded-lg shadow-md bg-white">
          <h2 className="text-xl font-bold mb-2">Booking Details</h2>
          <p><strong>Slot ID:</strong> {bookingDetails.slotId}</p>
          <p><strong>Price:</strong> ${bookingDetails.price}</p>
          <button
            className="bg-green-500 text-white px-6 py-2 mt-4 rounded-lg shadow-md hover:bg-green-600"
            onClick={handlePayment}
            disabled={paymentProcessing}
          >
            Pay and Exit
          </button>
        </div>
      )}
    </div>
  );
};

export default Exit;
