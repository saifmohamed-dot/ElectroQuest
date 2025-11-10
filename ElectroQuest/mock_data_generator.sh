#!/bin/bash

GA_FILE="ga_data.json"
PSI_FILE="psi_data.json"

# 20 realistic web page paths
PAGES=(
  "/home" "/about" "/contact" "/pricing" "/features"
  "/testimonials" "/blog" "/careers" "/faq" "/support"
  "/products" "/product-details" "/cart" "/checkout" "/orders"
  "/login" "/register" "/dashboard" "/settings" "/profile"
)

# Date range (last 30 days)
DAYS_RANGE=30

rm -f "$GA_FILE" "$PSI_FILE"

# Helper: random float
rand_float() {
  local min=$1
  local max=$2
  awk -v min="$min" -v max="$max" 'BEGIN{srand(); printf("%.2f", min+rand()*(max-min))}'
}

# Helper: random integer
rand_int() {
  local min=$1
  local max=$2
  echo $((RANDOM % (max - min + 1) + min))
}

# --------------------------
# Build list of all (date,page) pairs
# --------------------------
pairs=()
for ((d=0; d<DAYS_RANGE; d++)); do
  date=$(date -d "-$d days" +"%Y-%m-%d")
  for page in "${PAGES[@]}"; do
    pairs+=("$page|$date")
  done
done

NUM_RECORDS=${#pairs[@]}
echo "📅 Generating $NUM_RECORDS records ($DAYS_RANGE days × ${#PAGES[@]} pages)"

# --------------------------
# Shuffle pairs for both GA and PSI
# --------------------------
shuffled_pairs_ga=($(printf "%s\n" "${pairs[@]}" | shuf))
shuffled_pairs_psi=($(printf "%s\n" "${pairs[@]}" | shuf))

# --------------------------
# Generate GA JSON (shuffled order)
# --------------------------
echo "[" > "$GA_FILE"
for ((i=0; i<NUM_RECORDS; i++)); do
  IFS='|' read -r page date <<< "${shuffled_pairs_ga[$i]}"
  users=$((RANDOM % 951 + 50))
  sessions=$((RANDOM % 1141 + 60))
  views=$((RANDOM % 2901 + 100))

  printf '  {"date": "%s", "page": "%s", "users": %d, "sessions": %d, "views": %d}' \
    "$date" "$page" "$users" "$sessions" "$views" >> "$GA_FILE"

  if [ $i -lt $((NUM_RECORDS - 1)) ]; then
    echo "," >> "$GA_FILE"
  else
    echo >> "$GA_FILE"
  fi
done
echo "]" >> "$GA_FILE"

# --------------------------
# Generate PSI JSON (integers for LCP_ms)
# --------------------------
echo "[" > "$PSI_FILE"
for ((i=0; i<NUM_RECORDS; i++)); do
  IFS='|' read -r page date <<< "${shuffled_pairs_psi[$i]}"
  perf_score=$(rand_float 0.4 1.0)
  lcp_ms=$(rand_int 800 3500) # random integer between 800 and 3500 ms

  printf '  {"date": "%s", "page": "%s", "performanceScore": %s, "LCP_ms": %d}' \
    "$date" "$page" "$perf_score" "$lcp_ms" >> "$PSI_FILE"

  if [ $i -lt $((NUM_RECORDS - 1)) ]; then
    echo "," >> "$PSI_FILE"
  else
    echo >> "$PSI_FILE"
  fi
done
echo "]" >> "$PSI_FILE"

echo "✅ Generated:"
echo " - $GA_FILE (shuffled order)"
echo " - $PSI_FILE (different shuffled order, integer LCP_ms)"
