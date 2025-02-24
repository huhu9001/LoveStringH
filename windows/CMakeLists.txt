cmake_minimum_required(VERSION 3.28)
project(LoveStringH VERSION 1.2)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED True)

if(NOT CMAKE_BUILD_TYPE)
    set(CMAKE_BUILD_TYPE Release)
endif()

set(CMAKE_CXX_FLAGS "-Wall -Wextra")
set(CMAKE_CXX_FLAGS_DEBUG "-g")
set(CMAKE_CXX_FLAGS_RELEASE "-O3")

include(path.cmake OPTIONAL)

find_path(DIRINCLUDE_BOOST_REGEX boost/regex.hpp)
if (DIRINCLUDE_BOOST_REGEX STREQUAL DIRINCLUDE_BOOST_REGEX-NOTFOUND)
    message(SEND_ERROR "DIRINCLUDE_BOOST_REGEX: boost/regex header not found.")
    message("=>https://github.com/boostorg/regex")
endif()
find_path(DIRINCLUDE_CTRE ctre.hpp)
if (DIRINCLUDE_CTRE STREQUAL DIRINCLUDE_CTRE-NOTFOUND)
    message(SEND_ERROR "DIRINCLUDE_CTRE: ctre header not found.")
    message("=>https://github.com/hanickadot/compile-time-regular-expressions")
endif()
find_path(DIRLIB_WIN32 Comctl32.Lib)
if (DIRLIB_WIN32 STREQUAL DIRLIB_WIN32-NOTFOUND)
    message(SEND_ERROR "DIRLIB_WIN32: win32 lib not found.")
    message("=>Windows Kit")
endif()

add_library(Comctl32 SHARED IMPORTED)
set_property(TARGET Comctl32 PROPERTY IMPORTED_IMPLIB ${DIRLIB_WIN32}/Comctl32.lib)

include_directories(${DIRINCLUDE_BOOST_REGEX} ${DIRINCLUDE_CTRE})
link_libraries(Comctl32)

add_executable(LoveStringH
    encoder.cpp
    translit_latin.cpp
    translit_greek.cpp
    translit_cyrillic.cpp
    translit_arabic.cpp
    translit_hangul.cpp
    win32/encoder_win32.cpp
    win32/main_win32.cpp)