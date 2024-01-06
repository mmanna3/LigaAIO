# Stop any servers running on port 5001
ps aux | grep -i xsp4.*42000 | awk '{print $2}' | xargs kill -9

cd LigaSoft && xsp4 --address 0.0.0.0 --port 42000 --nonstop & 

# Espera para asegurarse que el servidor haya arrancado posta
sleep 2s

open -na "Google Chrome" --args --incognito http://0.0.0.0:42000/torneo &
#open http://0.0.0.0:42000/torneo &