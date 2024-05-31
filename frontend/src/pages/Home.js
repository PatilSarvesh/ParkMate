import { useNavigate } from 'react-router-dom';

const Home = () => {
  const navigate = useNavigate();

  return (
    <div className="flex flex-col items-center justify-center h-screen bg-gray-100 dark:bg-gray-900">
      <h1 className="text-4xl font-bold mb-8 text-gray-900 dark:text-gray-200">Welcome to Parking App</h1>
      <div className="flex space-x-8">
        <button
          className="bg-blue-500 text-white px-8 py-4 rounded-lg shadow-md hover:bg-blue-600 transition duration-200"
          onClick={() => navigate('/park')}
        >
          Park
        </button>
        <button
          className="bg-red-500 text-white px-8 py-4 rounded-lg shadow-md hover:bg-red-600 transition duration-200"
          onClick={() => navigate('/exit')}
        >
          Exit
        </button>
      </div>
    </div>
  );
};

export default Home;

