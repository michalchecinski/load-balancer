docker kill $(docker ps -q)

docker run -p 5000:5000 -e INSTANCE='1' -d -v $PWD/src:/src service

docker run -p 5001:5000 -e INSTANCE='2' -d -v $PWD/src:/src service